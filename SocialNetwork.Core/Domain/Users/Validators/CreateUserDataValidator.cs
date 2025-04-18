﻿using FluentValidation;
using FluentValidation.Results;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Data;
using SocialNetwork.Core.Domain.Users.Rules;

namespace SocialNetwork.Core.Domain.Users.Validators;

public class CreateUserDataValidator : AbstractValidator<CreateUserData>
{
    public CreateUserDataValidator(IEmailMustBeUniqueChecker emailChecker)
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage($"{nameof(CreateUserData.UserName)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(CreateUserData.UserName)} cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage($"{nameof(CreateUserData.Email)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(CreateUserData.Email)} cannot exceed 50 characters.")
            .EmailAddress().WithMessage($"{nameof(CreateUserData.Email)} must be a valid email address.")
            .CustomAsync(async (email, context, cancellationToken) =>
            {
                var checkResult = await new EmailMustBeUniqueBusinessRule(email, emailChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateUserData.Email), error));
                }
            });

        RuleFor(x => x.ProfilePicturePath)
            .NotEmpty().WithMessage($"{nameof(CreateUserData.ProfilePicturePath)} is required.")
            .Must(path => Uri.IsWellFormedUriString(path, UriKind.Absolute))
            .WithMessage($"{nameof(CreateUserData.ProfilePicturePath)} must be a valid absolute URL.");

        RuleFor(x => x.Bio)
            .MaximumLength(300).WithMessage($"{nameof(CreateUserData.Bio)} cannot exceed 300 characters.");
    }
}



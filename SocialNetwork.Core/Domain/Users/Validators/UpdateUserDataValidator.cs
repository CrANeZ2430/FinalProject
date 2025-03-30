using FluentValidation;
using FluentValidation.Results;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Data;
using SocialNetwork.Core.Domain.Users.Rules;

namespace SocialNetwork.Core.Domain.Users.Validators;

public class UpdateUserDataValidator : AbstractValidator<UpdateUserData>
{
    public UpdateUserDataValidator(IEmailMustBeUniqueChecker emailChecker)
    {
        RuleFor(x => x.UserName)
    .NotEmpty().WithMessage($"{nameof(UpdateUserData.UserName)} is required.")
    .MaximumLength(50).WithMessage($"{nameof(UpdateUserData.UserName)} cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage($"{nameof(UpdateUserData.Email)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(UpdateUserData.Email)} cannot exceed 50 characters.")
            .EmailAddress().WithMessage($"{nameof(UpdateUserData.Email)} must be a valid email address.")
            .CustomAsync(async (email, context, cancellationToken) =>
            {
                var checkResult = await new EmailMustBeUniqueBusinessRule(email, emailChecker).CheckAsync(cancellationToken);

                if (checkResult.IsSuccess) return;

                foreach (var error in checkResult.Errors)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateUserData.Email), error));
                }
            });

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage($"{nameof(UpdateUserData.PasswordHash)} is required.")
            .MinimumLength(6).WithMessage($"{nameof(UpdateUserData.PasswordHash)} must be at least 6 characters long.");

        RuleFor(x => x.ProfilePicturePath)
            .NotEmpty().WithMessage($"{nameof(UpdateUserData.ProfilePicturePath)} is required.")
            .Must(path => Uri.IsWellFormedUriString(path, UriKind.Relative))
            .WithMessage($"{nameof(UpdateUserData.ProfilePicturePath)} must be a valid relative URL.");

        RuleFor(x => x.Bio)
            .MaximumLength(300).WithMessage($"{nameof(UpdateUserData.Bio)} cannot exceed 300 characters.");

    }
}


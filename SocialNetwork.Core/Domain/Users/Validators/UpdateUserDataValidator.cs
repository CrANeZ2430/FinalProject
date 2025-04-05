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

        //RuleFor(x => x.ProfilePicturePath)
        //    .NotEmpty().WithMessage($"{nameof(UpdateUserData.ProfilePicturePath)} is required.")
        //    .Must(path => Uri.IsWellFormedUriString(path, UriKind.Absolute))
        //    .WithMessage($"{nameof(UpdateUserData.ProfilePicturePath)} must be a valid absolute URL.");

        RuleFor(x => x.Bio)
            .MaximumLength(300).WithMessage($"{nameof(UpdateUserData.Bio)} cannot exceed 300 characters.");

    }
}


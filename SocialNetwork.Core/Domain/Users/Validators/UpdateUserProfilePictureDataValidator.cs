using FluentValidation;
using SocialNetwork.Core.Domain.Users.Data;

namespace SocialNetwork.Core.Domain.Users.Validators;

public class UpdateUserProfilePictureDataValidator : AbstractValidator<UpdateUserProfilePictureData>
{
    public UpdateUserProfilePictureDataValidator()
    {
        RuleFor(x => x.ProfilePicturePath)
            .NotEmpty().WithMessage($"{nameof(UpdateUserProfilePictureData.ProfilePicturePath)} is required.")
            .Must(path => Uri.IsWellFormedUriString(path, UriKind.Absolute))
            .WithMessage($"{nameof(UpdateUserProfilePictureData.ProfilePicturePath)} must be a valid absolute URL.");
    }
}

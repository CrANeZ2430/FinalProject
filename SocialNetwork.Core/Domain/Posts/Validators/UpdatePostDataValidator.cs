using FluentValidation;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Core.Domain.Posts.Validators;

public class UpdatePostDataValidator : AbstractValidator<UpdatePostData>
{
    public UpdatePostDataValidator(IUsersRepository usersRepository)
    {
        RuleFor(post => post.Title)
            .NotEmpty().WithMessage($"{nameof(UpdatePostData.Title)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(UpdatePostData.Title)} must be at most 50 characters.");

        RuleFor(post => post.Content)
            .NotEmpty().WithMessage($"{nameof(UpdatePostData.Content)} is required.")
            .MinimumLength(10).WithMessage($"{nameof(UpdatePostData.Content)} must be at least 10 characters.")
            .MaximumLength(500).WithMessage($"{nameof(UpdatePostData.Content)} must be at most 500 characters.");

        RuleFor(post => post.ImagePath)
            .Must(paths => paths == null || paths.All(p => Uri.IsWellFormedUriString(p, UriKind.Absolute)))
            .WithMessage($"Each {nameof(UpdatePostData.ImagePath)} must be a valid absolute URI.");
    }
}

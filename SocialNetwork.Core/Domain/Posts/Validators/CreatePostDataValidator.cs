using FluentValidation;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Core.Domain.Posts.Validators;

public class CreatePostDataValidator : AbstractValidator<CreatePostData>
{
    public CreatePostDataValidator(IUsersRepository usersRepository)
    {
        RuleFor(post => post.Title)
            .NotEmpty().WithMessage($"{nameof(CreatePostData.Title)} is required.")
            .MaximumLength(50).WithMessage($"{nameof(CreatePostData.Title)} must be at most 50 characters.");

        RuleFor(post => post.Content)
            .NotEmpty().WithMessage($"{nameof(CreatePostData.Content)} is required.")
            .MinimumLength(10).WithMessage($"{nameof(CreatePostData.Content)} must be at least 10 characters.")
            .MaximumLength(500).WithMessage($"{nameof(CreatePostData.Content)} must be at at most 500 characters.");

        RuleFor(post => post.ImagePath)
            .Must(paths => paths == null || paths.All(p => Uri.IsWellFormedUriString(p, UriKind.Absolute)))
            .WithMessage($"Each {nameof(CreatePostData.ImagePath)} must be a valid absolute URI.");

        RuleFor(post => post.UserId)
            .CustomAsync(async (userId, context, cancellationToken) =>
            {
                var userExists = await usersRepository.UserExists(userId, cancellationToken);
                if (userExists) return;
                context.AddFailure($"{nameof(CreatePostData.UserId)} does not exist.");
            });
    }
}

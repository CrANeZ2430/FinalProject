using FluentValidation;
using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Core.Domain.Comments.Validator;

public class CreateCommentDataValidator : AbstractValidator<CreateCommentData>
{
    public CreateCommentDataValidator(
        IUsersRepository usersRepository,
        IPostsRepository postsRepository)
    {
        RuleFor(comment => comment.Content)
            .NotEmpty().WithMessage($"{nameof(CreateCommentData.Content)} is required.")
            .MaximumLength(1000).WithMessage($"{nameof(CreateCommentData.Content)} must be less than 1000 characters.");

        RuleFor(comment => comment.UserId)
            .NotNull().WithMessage($"{nameof(CreateCommentData.UserId)} is required.")
            .CustomAsync(async (userId, context, cancellationToken) =>
            {
                var userExists = await usersRepository.UserExists(userId, cancellationToken);
                if (userExists) return;
                context.AddFailure($"{nameof(CreateCommentData.UserId)} does not exist.");
            }); ;

        RuleFor(comment => comment.PostId)
            .NotEmpty().WithMessage($"{nameof(CreateCommentData.PostId)} is required.")
            .CustomAsync(async (postId, context, cancellationToken) =>
            {
                var userExists = await postsRepository.PostExists(postId, cancellationToken);
                if (userExists) return;
                context.AddFailure($"{nameof(CreateCommentData.PostId)} does not exist.");
            });
    }
}

using FluentValidation;
using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Core.Domain.Comments.Validator;

public class UpdateCommentDataValidator : AbstractValidator<UpdateCommentData>
{
    public UpdateCommentDataValidator()
    {
        RuleFor(comment => comment.Content)
            .NotEmpty().WithMessage($"{nameof(UpdateCommentData.Content)} is required.")
            .MaximumLength(1000).WithMessage($"{nameof(UpdateCommentData.Content)} must be less than 1000 characters.");

    }
}

using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;

namespace SocialNetwork.Application.Domain.Comments.Commands.LikeComment;

public class LikeCommentCommandHandler(
    ICommentsRepository commentsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LikeCommentCommand>
{
    public async Task Handle(
        LikeCommentCommand command,
        CancellationToken cancellationToken = default)
    {
        var comment = await commentsRepository
                .GetById(command.CommentId, cancellationToken);

        comment.LikeComment(command.IsLike);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

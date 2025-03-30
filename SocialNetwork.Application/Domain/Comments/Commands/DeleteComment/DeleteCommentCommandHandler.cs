using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;

namespace SocialNetwork.Application.Domain.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler(
    ICommentsRepository commentsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCommentCommand>
{
    public async Task Handle(
        DeleteCommentCommand command, 
        CancellationToken cancellationToken = default)
    {
        var comment = await commentsRepository.GetById(command.CommentId, cancellationToken);

        commentsRepository.Remove(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

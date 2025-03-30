using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Comments.Data;

namespace SocialNetwork.Application.Domain.Comments.Commands.UpdateComment;

public class UpdateCommentCommandHandler(
    ICommentsRepository commentsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCommentCommand>
{
    public async Task Handle(
        UpdateCommentCommand command, 
        CancellationToken cancellationToken)
    {
        var comment = await commentsRepository.GetById(command.CommentId, cancellationToken);

        var data = new UpdateCommentData(
            command.Content);

        await comment.Update(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

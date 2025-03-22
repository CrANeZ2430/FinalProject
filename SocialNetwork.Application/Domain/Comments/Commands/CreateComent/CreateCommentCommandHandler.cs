using MediatR;
using SocialNetwork.Core.Common.DbContext;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Comments.Models;

namespace SocialNetwork.Application.Domain.Comments.Commands.CreatePost;

public class CreateCommentCommandHandler(
    ICommentsRepository commentsRepository,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCommentCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateCommentCommand command, 
        CancellationToken cancellationToken = default)
    {
        var data = new CreateCommentData(
            command.UserId,
            command.PostId,
            command.Content);

        var comment = Comment.Create(data);
        commentsRepository.Add(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.CommentId;
    }
}

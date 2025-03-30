using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Comments.Commands.CreatePost;

public class CreateCommentCommandHandler(
    IUsersRepository usersRepository,
    IPostsRepository postsRepository,
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

        var comment = await Comment.Create(
            data, 
            usersRepository, 
            postsRepository);

        commentsRepository.Add(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return comment.CommentId;
    }
}

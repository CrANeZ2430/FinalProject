using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Posts.Common;

namespace SocialNetwork.Application.Domain.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(
    IPostsRepository postsRepository,
    ICommentsRepository commentsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(
        DeletePostCommand command, 
        CancellationToken cancellationToken = default)
    {
        var post = await postsRepository.GetById(command.PostId, cancellationToken);

        var comments = await commentsRepository
            .GetAll(command.PostId, cancellationToken);

        foreach (var comment in comments)
            commentsRepository.Remove(comment);

        postsRepository.Remove(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

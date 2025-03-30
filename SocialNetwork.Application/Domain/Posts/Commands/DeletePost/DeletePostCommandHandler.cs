using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Posts.Common;

namespace SocialNetwork.Application.Domain.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(
    IPostsRepository postsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(
        DeletePostCommand command, 
        CancellationToken cancellationToken = default)
    {
        var post = await postsRepository.GetById(command.PostId, cancellationToken);

        postsRepository.Remove(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Posts.Common;

namespace SocialNetwork.Application.Domain.Posts.Commands.AddLike;

public class LikePostCommandHandler(
    IPostsRepository postsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LikePostCommand>
{
    public async Task Handle(
        LikePostCommand command, 
        CancellationToken cancellationToken = default)
    {
        var post = await postsRepository
                .GetById(command.PostId, cancellationToken);

        post.LikePost(command.IsLike);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

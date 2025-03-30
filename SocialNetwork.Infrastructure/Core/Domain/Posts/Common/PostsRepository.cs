using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Core.Exceptions;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Domain.Posts.Common;

internal class PostsRepository(SocialNetworkDbContext dbContext) : IPostsRepository
{
    public void Add(Post post)
    {
        dbContext.Posts.Add(post);
    }

    public void Remove(Post post)
    {
        dbContext.Posts.Remove(post);
    }

    public async Task<Post> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Posts
                    .FirstOrDefaultAsync(x => x.PostId == id, cancellationToken)
                    ?? throw new NotFoundException($"{nameof(Post)} cannot be found."); ;
    }

    public async Task<bool> PostExists(Guid postId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Posts
                    .AnyAsync(x => x.PostId == postId, cancellationToken);
    }
}

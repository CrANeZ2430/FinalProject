using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Posts.Common;

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
                    ?? throw new InvalidOperationException($"{nameof(Post)} cannot be found");
    }
}

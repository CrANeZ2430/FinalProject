using SocialNetwork.Core.Domain.Posts.Models;

namespace SocialNetwork.Core.Domain.Posts.Common;

public interface IPostsRepository
{
    void Add(Post post);
    void Remove(Post post);
    Task<Post> GetById(Guid postId, CancellationToken cancellationToken = default);
}

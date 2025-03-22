using SocialNetwork.Core.Domain.Comments.Models;

namespace SocialNetwork.Core.Domain.Comments.Common;

public interface ICommentsRepository
{
    void Add(Comment comment);
    void Remove(Comment comment);
    Task<Comment> GetById(Guid commentId, CancellationToken cancellationToken = default);
}

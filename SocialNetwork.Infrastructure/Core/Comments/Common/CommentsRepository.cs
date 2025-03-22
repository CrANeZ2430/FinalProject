using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Comments.Common;

internal class CommentsRepository(SocialNetworkDbContext dbContext) : ICommentsRepository
{
    public void Add(Comment comment)
    {
        dbContext.Comments.Add(comment);
    }

    public void Remove(Comment comment)
    {
        dbContext.Remove(comment);
    }

    public async Task<Comment> GetById(Guid commentId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Comments
                    .FirstOrDefaultAsync(x => x.CommentId == commentId, cancellationToken)
                    ?? throw new InvalidOperationException($"{nameof(Comment)} cannot be found");
    }
}

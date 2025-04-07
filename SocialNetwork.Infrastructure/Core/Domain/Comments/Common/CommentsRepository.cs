using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Core.Exceptions;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Domain.Comments.Common;

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
                    ?? throw new NotFoundException($"{nameof(Comment)} cannot be found."); ;
    }

    public async Task<IEnumerable<Comment>> GetAll(Guid postId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Comments
                    .Where(x => x.PostId == postId)
                    .ToListAsync(cancellationToken);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Comments.Queries.GetPostComments;

public class GetPostCommentsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetPostCommentsQuery, PageResponse<PostDto>>
{
    public async Task<PageResponse<PostDto>> Handle(
        GetPostCommentsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var postEntity = await dbContext.Posts
            .Include(p => p.User)
            .Where(p => p.PostId == query.PostId)
            .FirstOrDefaultAsync(cancellationToken);

        var commentsQuery = dbContext.Comments
            .Where(c => c.PostId == query.PostId)
            .OrderByDescending(c => c.CreationTime)
            .Include(c => c.User)
            .Select(c => new CommentDto(
                c.CommentId,
                c.User != null
                    ? new UserDto(c.User.UserName, c.User.ProfilePicturePath)
                    : null,
                c.Content,
                c.LikeCount,
                c.CreationTime
            ));

        var totalCount = await commentsQuery.CountAsync(cancellationToken);

        var comments = await commentsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToArrayAsync(cancellationToken);

        var post = new PostDto(
            postEntity.PostId,
            postEntity.Title,
            postEntity.Content,
            postEntity.LikeCount,
            postEntity.CreationTime,
            new UserDto(
                postEntity.User.UserName, 
                postEntity.User.ProfilePicturePath),
            comments
        );

        return new PageResponse<PostDto>(totalCount, post);
    }
}

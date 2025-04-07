using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Posts.Queries.GetUserPosts;

public class GetUserPostsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUserPostsQuery, PageResponse<PostDto[]>>
{
    public async Task<PageResponse<PostDto[]>> Handle(
        GetUserPostsQuery query,
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = dbContext.Posts
        .AsNoTracking()
        .Where(c => c.UserId == query.UserId);

        var totalCount = await sqlQuery.CountAsync(cancellationToken);

        var posts = await sqlQuery
            .OrderBy(c => c.CreationTime)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PostDto(
                p.PostId,
                p.Title,
                p.Content,
                p.LikeCount,
                p.Comments.Count(),
                p.CreationTime,
                //p.UpdateTime,
                new UserDto(
                    p.User.UserId,
                    p.User.UserName,
                    p.User.ProfilePicturePath)))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<PostDto[]>(totalCount, posts);
    }
}

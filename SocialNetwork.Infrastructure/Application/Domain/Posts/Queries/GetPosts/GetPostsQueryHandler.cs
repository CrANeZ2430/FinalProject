using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Posts.Queries.GetPosts;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Posts.Queries.GetPosts;

public class GetPostsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetPostsQuery, PageResponse<PostDto[]>>
{
    public async Task<PageResponse<PostDto[]>> Handle(
        GetPostsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var skip = query.PageSize * (query.Page - 1);

        var sqlQuery = dbContext.Posts
            .AsNoTracking()
            .OrderBy(p => p.Title)
            .Select(p => new PostDto(
                p.PostId,
                p.Title,
                p.Content,
                p.ImagePath,
                p.CreationTime,
                p.UpdateTime,
                new UserDto(
                    p.User.UserName,
                    p.User.Email,
                    p.User.PasswordHash),
                p.Comments
                    .Select(c => new CommentDto(
                        new UserDto(
                            c.User.UserName,
                            c.User.Email,
                            c.User.PasswordHash),
                        c.Content))
                    .ToArray()
            ))
            .Skip(skip)
            .Take(query.PageSize);

        var count = await dbContext.Posts
                .CountAsync(cancellationToken);
        var posts = await sqlQuery
                .ToArrayAsync(cancellationToken);

        return new PageResponse<PostDto[]>(count, posts);

    }
}

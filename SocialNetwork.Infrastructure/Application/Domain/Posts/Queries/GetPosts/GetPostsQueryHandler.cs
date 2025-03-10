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
        GetPostsQuery request, 
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = dbContext
            .Posts
            .AsNoTracking()
            .Include(x => x.User);

        var skip = request.PageSize * (request.Page - 1);
        var count = sqlQuery.Count();

        var users = await sqlQuery
            .OrderBy(x => x.Title)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new PostDto(
                x.PostId,
                x.Title,
                x.Content,
                x.ImagePath,
                x.CreationTime,
                x.UpdateTime,
                new UserDto(
                    x.User.UserName,
                    x.User.Email,
                    x.User.PasswordHash)
                ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<PostDto[]>(count, users);
    }
}

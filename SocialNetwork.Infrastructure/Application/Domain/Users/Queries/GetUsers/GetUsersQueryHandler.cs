using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Users.Queries.GetUsers;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Users.Queries.GetUsers;

internal class GetUsersQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUsersQuery, PageResponse<UserDto[]>>
{
    public async Task<PageResponse<UserDto[]>> Handle(
        GetUsersQuery query, 
        CancellationToken cancellationToken = default)
    {
        var skip = query.PageSize * (query.Page - 1);

        var sqlQuery = dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.UserName)
            .Select(u => new UserDto(
                u.UserId,
                u.UserName,
                u.Email,
                u.ProfilePicturePath,
                u.Bio, 
                u.Posts.Count(),
                u.Comments.Count(),
                u.CreationTime))
            .Skip(skip)
            .Take(query.PageSize);

        var count = await dbContext.Users
                .CountAsync(cancellationToken);
        var users = await sqlQuery
                .ToArrayAsync(cancellationToken);

        return new PageResponse<UserDto[]>(count, users);

    }
}

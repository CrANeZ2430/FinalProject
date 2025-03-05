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
        GetUsersQuery request, 
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = dbContext
            .Users
            .AsNoTracking();

        var skip = request.PageSize * (request.PageSize - 1);
        var count = sqlQuery.Count();

        var users = await sqlQuery
            .OrderBy(x => x.UserName)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new UserDto(
                x.UserId,
                x.UserName,
                x.Email,
                x.Password,
                x.ProfilePicturePath,
                x.Bio,
                x.CreationTime))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<UserDto[]>(count, users);
    }
}

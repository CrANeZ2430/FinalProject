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
            .AsNoTracking()
            .Include(x => x.Posts);

        var skip = request.PageSize * (request.Page - 1);
        var count = sqlQuery.Count();

        var users = await sqlQuery
            .OrderBy(x => x.UserName)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new UserDto(
                x.UserId,
                x.UserName,
                x.Email,
                x.PasswordHash,
                x.ProfilePicturePath,
                x.Bio,
                x.CreationTime,
                x.Posts
                .Select(x => new PostDto(
                    x.Title,
                    x.Content,
                    x.ImagePath))
                .ToArray()
                ))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<UserDto[]>(count, users);
    }
}

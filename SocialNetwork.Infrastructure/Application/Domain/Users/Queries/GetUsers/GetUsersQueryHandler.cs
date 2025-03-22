using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Users.Queries.GetUsers;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Users.Queries.GetUsers;

internal class GetUsersQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUsersQuery, PageResponse<PostUserDto[]>>
{
    public async Task<PageResponse<PostUserDto[]>> Handle(
        GetUsersQuery query, 
        CancellationToken cancellationToken = default)
    {
        var skip = query.PageSize * (query.Page - 1);

        var sqlQuery = dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.UserName)
            .Select(u => new PostUserDto(
                u.UserId,
                u.UserName,
                u.Email,
                u.PasswordHash,
                u.ProfilePicturePath,
                u.Bio,
                u.CreationTime,
                u.Posts
                    .Select(p => new PostDto(
                        p.Title,
                        p.Content,
                        p.ImagePath,
                        p.Comments
                            .Select(c => new CommentDto(
                                new CommentUserDto(
                                    c.User.UserId,
                                    c.User.UserName,
                                    c.User.Email,
                                    c.User.PasswordHash,
                                    c.User.ProfilePicturePath,
                                    c.User.Bio,
                                    c.User.CreationTime
                                ),
                                c.Content))
                            .ToArray()))
                    .ToArray()
            ))
            .Skip(skip)
            .Take(query.PageSize);

        var count = await dbContext.Users
                .CountAsync(cancellationToken);
        var users = await sqlQuery
                .ToArrayAsync(cancellationToken);

        return new PageResponse<PostUserDto[]>(count, users);

    }
}

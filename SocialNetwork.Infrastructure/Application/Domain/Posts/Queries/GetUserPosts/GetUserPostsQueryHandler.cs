using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Posts.Queries.GetUserPosts;

public class GetUserPostsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUserPostsQuery, PageResponse<UserDto>>
{
    public async Task<PageResponse<UserDto>> Handle(
        GetUserPostsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users
                .AsNoTracking()
                .Where(u => u.UserId == query.UserId)
                .Select(u => new UserDto(
                    u.UserId,
                    u.UserName,
                    u.Email,
                    u.ProfilePicturePath,
                    u.Bio,
                    u.CreationTime,
                    u.Posts
                        .OrderBy(p => p.CreationTime)
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .Select(p => new PostDto(
                            p.PostId,
                            p.Title,
                            p.Content,
                            p.ImagePath,
                            p.LikeCount,
                            p.CreationTime,
                            p.UpdateTime))
                        .ToArray()
                ))
                .FirstOrDefaultAsync(cancellationToken);

        return new PageResponse<UserDto>(user.Posts.Length, user);
    }
}

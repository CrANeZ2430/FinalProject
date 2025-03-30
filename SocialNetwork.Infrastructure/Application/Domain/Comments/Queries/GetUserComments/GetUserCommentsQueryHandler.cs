using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Comments.Queries.GetUserComments;

public class GetUserCommentsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUserCommentsQuery, PageResponse<UserDto>>
{
    public async Task<PageResponse<UserDto>> Handle(
        GetUserCommentsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Where(u => u.UserId == query.UserId)
            .Select(u => new UserDto(
                u.UserId,
                u.UserName,
                u.Email,
                u.PasswordHash,
                u.ProfilePicturePath,
                u.Bio,
                u.CreationTime,
                u.Comments
                    .OrderBy(c => c.CreationTime)
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(c => new CommentDto(
                        c.CommentId,
                        c.Content,
                        c.LikeCount,
                        c.CreationTime,
                        c.UpdateTime,
                        c.PostId))
                    .ToArray()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return new PageResponse<UserDto>(user.Comments.Length, user);
    }
}

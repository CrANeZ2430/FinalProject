using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Comments.Queries.GetUserComments;

public class GetUserCommentsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetUserCommentsQuery, PageResponse<CommentDto[]>>
{
    public async Task<PageResponse<CommentDto[]>> Handle(
        GetUserCommentsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = dbContext.Comments
        .AsNoTracking()
        .Where(c => c.UserId == query.UserId);

        var totalCount = await sqlQuery.CountAsync(cancellationToken);

        var comments = await sqlQuery
            .OrderBy(c => c.CreationTime)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new CommentDto(
                c.CommentId,
                c.Content,
                c.LikeCount,
                c.CreationTime,
                c.UpdateTime,
                c.PostId,
                new UserDto(
                    c.User.UserName,
                    c.User.ProfilePicturePath)))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<CommentDto[]>(totalCount, comments);
    }
}

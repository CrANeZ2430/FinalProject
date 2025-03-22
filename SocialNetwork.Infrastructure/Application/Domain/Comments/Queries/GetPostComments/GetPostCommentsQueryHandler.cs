using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Common;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Application.Domain.Comments.Queries.GetPostComments;

public class GetPostCommentsQueryHandler(
    SocialNetworkDbContext dbContext)
    : IRequestHandler<GetPostCommentsQuery, PageResponse<CommentDto[]>>
{
    public async Task<PageResponse<CommentDto[]>> Handle(
        GetPostCommentsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = dbContext.Comments
                .AsNoTracking()
                .Where(c => c.PostId == query.PostId)
                .OrderBy(c => c.CreationTime)
                .Select(c => new CommentDto(
                    c.CommentId,
                    new UserDto(
                        c.User.UserName,
                        c.User.Email,
                        c.User.PasswordHash),
                    c.Content));

        var count = await sqlQuery.CountAsync(cancellationToken);

        var comments = await sqlQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToArrayAsync(cancellationToken);

        return new PageResponse<CommentDto[]>(count, comments);
    }
}

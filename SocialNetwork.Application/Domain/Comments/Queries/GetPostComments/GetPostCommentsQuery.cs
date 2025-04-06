using MediatR;
using SocialNetwork.Application.Common;

namespace SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;

public record GetPostCommentsQuery(
    Guid PostId,
    int Page,
    int PageSize)
    : IRequest<PageResponse<PostDto>>;

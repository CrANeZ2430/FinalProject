using MediatR;
using SocialNetwork.Application.Common;

namespace SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;

public record GetUserPostsQuery(
    int Page,
    int PageSize,
    string UserId)
    : IRequest<PageResponse<PostDto[]>>;

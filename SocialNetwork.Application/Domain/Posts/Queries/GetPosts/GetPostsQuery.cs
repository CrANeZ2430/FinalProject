using MediatR;
using SocialNetwork.Application.Common;

namespace SocialNetwork.Application.Domain.Posts.Queries.GetPosts;

public record GetPostsQuery(
    int Page,
    int PageSize)
    : IRequest<PageResponse<PostDto[]>>;

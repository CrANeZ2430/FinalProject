using MediatR;
using SocialNetwork.Application.Common;

namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record GetUsersQuery(
    int Page,
    int PageSize) 
    : IRequest<PageResponse<UserDto[]>>;

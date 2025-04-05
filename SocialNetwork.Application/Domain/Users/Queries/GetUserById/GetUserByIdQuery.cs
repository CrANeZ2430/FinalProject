using MediatR;

namespace SocialNetwork.Application.Domain.Users.Queries.GetUserById;

public record GetUserByIdQuery(
    string UserId)
    : IRequest<UserDto>;

using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.DeleteUser;

public record DeleteUserCommand(
    Guid UserId)
    : IRequest;

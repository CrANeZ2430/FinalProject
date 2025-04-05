using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.DeleteUser;

public record DeleteUserCommand(
    string UserId)
    : IRequest;

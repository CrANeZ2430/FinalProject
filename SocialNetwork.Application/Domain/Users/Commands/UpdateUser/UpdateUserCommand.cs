using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.UpdateUser;

public record  UpdateUserCommand(
    string UserId,
    string UserName,
    string? Bio)
    : IRequest;

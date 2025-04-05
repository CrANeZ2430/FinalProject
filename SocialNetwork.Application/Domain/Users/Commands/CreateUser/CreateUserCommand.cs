using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.CreateUser;

public record CreateUserCommand(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio)
    : IRequest;

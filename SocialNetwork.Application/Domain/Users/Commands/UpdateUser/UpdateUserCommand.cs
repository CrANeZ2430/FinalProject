using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.UpdateUser;

public record  UpdateUserCommand(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio)
    : IRequest;

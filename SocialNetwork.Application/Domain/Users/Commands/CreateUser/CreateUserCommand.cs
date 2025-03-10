using MediatR;

namespace SocialNetwork.Application.Domain.Users.Commands.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string Bio) 
    : IRequest<Guid>;

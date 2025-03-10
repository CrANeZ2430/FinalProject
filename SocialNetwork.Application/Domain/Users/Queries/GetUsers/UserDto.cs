using MediatR;

namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record UserDto(
    Guid UserId,
    string UserName,
    string Email,
    string Password,
    string ProfilePicturePath,
    string Bio,
    DateTime CreationDate,
    PostDto[] Posts);

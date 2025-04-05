namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record PostUserDto(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationDate,
    PostDto[] Posts);

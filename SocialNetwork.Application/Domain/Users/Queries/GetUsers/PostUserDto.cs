namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record PostUserDto(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationDate,
    PostDto[] Posts);

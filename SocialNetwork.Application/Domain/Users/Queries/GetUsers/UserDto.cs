namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record UserDto(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio,
    int PostCount,
    int CommentCount,
    DateTime CreationDate);

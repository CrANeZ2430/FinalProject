namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record CommentUserDto(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationDate);

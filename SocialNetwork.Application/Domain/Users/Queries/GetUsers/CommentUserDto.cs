namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record CommentUserDto(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationDate);

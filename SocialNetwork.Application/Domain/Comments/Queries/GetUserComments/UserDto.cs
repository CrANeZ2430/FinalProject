namespace SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;

public record UserDto(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationTime,
    CommentDto[] Comments);
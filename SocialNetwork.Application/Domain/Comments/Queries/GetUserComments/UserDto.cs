namespace SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;

public record UserDto(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationTime,
    CommentDto[] Comments);
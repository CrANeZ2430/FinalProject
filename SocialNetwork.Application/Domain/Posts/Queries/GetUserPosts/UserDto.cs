namespace SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;

public record UserDto(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationTime,
    PostDto[] Posts);
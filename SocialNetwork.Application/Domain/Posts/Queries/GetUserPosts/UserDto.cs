namespace SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;

public record UserDto(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string? Bio,
    DateTime CreationTime,
    PostDto[] Posts);
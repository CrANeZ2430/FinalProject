namespace SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;

public record UserDto(
    string UserId,
    string UserName,
    string ProfilePicturePath);
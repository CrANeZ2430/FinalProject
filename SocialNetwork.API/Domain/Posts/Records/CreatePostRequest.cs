namespace SocialNetwork.API.Domain.Posts.Records;

public record CreatePostRequest(
    Guid UserId,
    string Title,
    string Content,
    string[]? ImagePath);

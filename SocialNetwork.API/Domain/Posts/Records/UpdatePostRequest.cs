namespace SocialNetwork.API.Domain.Posts.Records;

public record UpdatePostRequest(
    string Title,
    string Content,
    string[]? ImagePath);

namespace SocialNetwork.Core.Domain.Posts.Data;

public record CreatePostData(
    Guid UserId,
    string Title,
    string Content,
    string? ImagePath);

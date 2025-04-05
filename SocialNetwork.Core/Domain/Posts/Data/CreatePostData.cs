namespace SocialNetwork.Core.Domain.Posts.Data;

public record CreatePostData(
    string Title,
    string Content,
    string[]? ImagePath,
    string UserId);

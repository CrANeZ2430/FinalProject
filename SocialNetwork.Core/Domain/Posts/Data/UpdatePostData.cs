namespace SocialNetwork.Core.Domain.Posts.Data;

public record UpdatePostData(
    string Title,
    string Content,
    string[]? ImagePath);

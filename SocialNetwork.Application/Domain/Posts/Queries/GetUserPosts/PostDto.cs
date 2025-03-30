namespace SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;

public record PostDto(
    Guid PostId,
    string Title,
    string Content,
    string[]? ImagePath,
    int PostLikeCount,
    DateTime CreationDate,
    DateTime UpdateTime);
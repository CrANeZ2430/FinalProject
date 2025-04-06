namespace SocialNetwork.Application.Domain.Posts.Queries.GetPosts;

public record PostDto(
    Guid PostId,
    string Title,
    string Content,
    //string[]? ImagePath,
    int LikeCount,
    int CommentCount,
    DateTime CreationTime,
    //DateTime UpdateTime,
    UserDto? User);

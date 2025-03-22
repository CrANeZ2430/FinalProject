namespace SocialNetwork.Application.Domain.Posts.Queries.GetPosts;

public record PostDto(
    Guid PostId,
    string Title,
    string Content,
    string? ImagePath,
    DateTime CreationDate,
    DateTime UpdateTime,
    UserDto User,
    CommentDto[] Comments);

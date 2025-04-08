namespace SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;

public record PostDto(
    Guid PostId,
    string Title,
    string Content,
    string[]? ImagePath,
    int LikeCount,
    DateTime CreationTime,
    UserDto User,
    CommentDto[] Comments);

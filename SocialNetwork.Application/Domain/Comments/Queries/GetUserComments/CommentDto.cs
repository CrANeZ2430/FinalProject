namespace SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;

public record CommentDto(
    Guid CommentId,
    string Content,
    int LikeCount,
    DateTime CreationTime,
    DateTime UpdateTime,
    Guid PostId,
    UserDto User);

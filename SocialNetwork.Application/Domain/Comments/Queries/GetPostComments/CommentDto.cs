namespace SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;

public record CommentDto(
    Guid CommentId,
    UserDto? User,
    string Content,
    int LikeCount,
    DateTime CreationTime);

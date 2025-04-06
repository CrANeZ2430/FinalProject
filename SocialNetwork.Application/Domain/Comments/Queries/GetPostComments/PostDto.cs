namespace SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;

public record PostDto(
    Guid PostId,
    string Title,
    string Content,
    int LikeCount,
    DateTime CreationTime,
    UserDto User,
    CommentDto[] Comments);

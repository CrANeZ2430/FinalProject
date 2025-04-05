namespace SocialNetwork.Core.Domain.Comments.Data;

public record CreateCommentData(
    string UserId,
    Guid PostId,
    string Content);

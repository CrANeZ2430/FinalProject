namespace SocialNetwork.Core.Domain.Comments.Data;

public record CreateCommentData(
    Guid UserId,
    Guid PostId,
    string Content);

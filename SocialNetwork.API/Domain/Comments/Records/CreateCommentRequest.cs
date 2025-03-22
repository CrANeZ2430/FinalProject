namespace SocialNetwork.API.Domain.Comments.Records;

public record CreateCommentRequest(
    Guid UserId,
    Guid PostId,
    string Content);

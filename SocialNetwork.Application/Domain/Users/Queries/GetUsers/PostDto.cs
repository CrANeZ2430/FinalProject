namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record PostDto(
    string Title,
    string Content,
    string[]? ImagePath,
    int PostLikeCount,
    CommentDto[] Comments);

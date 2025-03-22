namespace SocialNetwork.Application.Domain.Posts.Queries.GetPosts;

public record CommentDto(
    UserDto? User,
    string Content,
    int CommentLikeCount);

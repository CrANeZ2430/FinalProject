namespace SocialNetwork.Application.Domain.Users.Queries.GetUsers;

public record CommentDto(
    CommentUserDto? userDto,
    string Content,
    int CommentLikeCount);

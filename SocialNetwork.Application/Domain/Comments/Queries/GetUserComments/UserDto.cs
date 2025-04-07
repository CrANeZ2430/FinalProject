namespace SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;

public record UserDto(
    string UserId,
    string UserName,
    string ProfilePicturePath);

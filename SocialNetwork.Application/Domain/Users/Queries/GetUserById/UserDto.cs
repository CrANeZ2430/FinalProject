namespace SocialNetwork.Application.Domain.Users.Queries.GetUserById;

public record UserDto(
    string UserName,
    string Email,
    string ProfilePicturePath,
    string Bio);

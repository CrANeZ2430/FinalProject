namespace SocialNetwork.Core.Domain.Users.Data;

public record CreateUserData(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string Bio);

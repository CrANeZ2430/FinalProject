namespace SocialNetwork.Core.Domain.Users.Data;

public record CreateUserData(
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string Bio);

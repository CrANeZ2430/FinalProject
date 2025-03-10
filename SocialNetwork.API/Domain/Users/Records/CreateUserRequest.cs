namespace SocialNetwork.API.Domain.Users.Records;

public record CreateUserRequest(
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string Bio);

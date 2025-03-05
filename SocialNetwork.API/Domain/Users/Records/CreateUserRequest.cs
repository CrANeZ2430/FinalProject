namespace SocialNetwork.API.Domain.Users.Records;

public record CreateUserRequest(
    string UserName,
    string Email,
    string Password,
    string ProfilePicturePath,
    string Bio);

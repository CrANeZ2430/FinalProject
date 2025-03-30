namespace SocialNetwork.API.Domain.Users.Records;

public record UpdateUserRequest(
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string? Bio);

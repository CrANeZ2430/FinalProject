using SocialNetwork.Core.Domain.Users.Data;

namespace SocialNetwork.Core.Domain.Users.Models;

public class User
{
    private User() { }

    public User(
        Guid userId, 
        string userName, 
        string email, 
        string password, 
        string profilePicturePath, 
        string bio, 
        DateTime creationTime)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        Password = password;
        ProfilePicturePath = profilePicturePath;
        Bio = bio;
        CreationTime = creationTime;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string ProfilePicturePath { get; private set; }
    public string Bio { get; private set; }
    public DateTime CreationTime { get; private set; }

    public static User Create(CreateUserData data)
    {
        return new User(
            Guid.NewGuid(),
            data.UserName,
            data.Email,
            data.Password,
            data.ProfilePicturePath,
            data.Bio,
            DateTime.Now);
    }
}

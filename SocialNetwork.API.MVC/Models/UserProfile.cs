namespace SocialNetwork.API.MVC.Models;

public class UserProfile
{
    public UserProfile(string userId, string name, string emailAddress, string profileImage)
    {
        UserId = userId;
        Name = name;
        EmailAddress = emailAddress;
        ProfileImage = profileImage;
    }

    public string UserId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string ProfileImage { get; set; }
}

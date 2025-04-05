namespace SocialNetwork.API.MVC.Models;

public record UserViewModel(
    string UserName,
    string ProfileImage,
    int PostCount);

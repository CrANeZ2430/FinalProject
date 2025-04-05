namespace SocialNetwork.API.MVC.Models;

public record UserProfileViewModel(
    string UserName,
    string Email,
    string ProfileImage,
    string Bio);

namespace SocialNetwork.API.MVC.Models;

public record UserProfileModel(
    string UserName,
    string Email,
    string ProfileImage,
    string Bio);

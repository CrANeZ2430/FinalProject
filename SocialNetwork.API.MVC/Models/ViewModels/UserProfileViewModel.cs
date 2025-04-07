namespace SocialNetwork.API.MVC.Models.ViewModels;

public record UserProfileViewModel(
    string UserId,
    string UserName,
    string Email,
    string ProfilePicturePath,
    string Bio);

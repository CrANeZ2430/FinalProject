namespace SocialNetwork.API.MVC.Models.ViewModels;

public record GeneralUserViewModel(
    string UserId,
    string UserName,
    string ProfilePicturePath,
    int PostCount,
    int CommentCount);

namespace SocialNetwork.API.MVC.Models.ViewModels;

public record AddPostFormViewModel(
    string Title,
    string Content,
    IFormFile[] Images);

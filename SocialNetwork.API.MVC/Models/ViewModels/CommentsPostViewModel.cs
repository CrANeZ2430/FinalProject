namespace SocialNetwork.API.MVC.Models.ViewModels;

public record CommentsPostViewModel(
    Guid PostId,
    string Title,
    string Content,
    string[]? Images,
    int LikeCount,
    DateTime CreationTime,
    UserViewModel User);

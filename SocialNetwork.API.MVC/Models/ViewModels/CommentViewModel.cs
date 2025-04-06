namespace SocialNetwork.API.MVC.Models.ViewModels;

public record CommentViewModel(
    Guid CommentId,
    string Content,
    int LikeCount,
    DateTime CreationTime,
    UserViewModel User);

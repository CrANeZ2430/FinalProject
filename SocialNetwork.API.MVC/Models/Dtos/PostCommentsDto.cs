using SocialNetwork.API.MVC.Models.ViewModels;

namespace SocialNetwork.API.MVC.Models.Dtos;

public record PostCommentsDto(
    PostViewModel Post,
    CommentViewModel[] Comments);

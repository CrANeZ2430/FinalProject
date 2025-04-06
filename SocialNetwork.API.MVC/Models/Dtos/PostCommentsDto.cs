using SocialNetwork.API.MVC.Models.ViewModels;

namespace SocialNetwork.API.MVC.Models.Dtos;

public record PostCommentsDto(
    CommentsPostViewModel Post,
    CommentViewModel[] Comments);

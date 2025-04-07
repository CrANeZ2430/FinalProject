using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models.ViewModels;
using SocialNetwork.Application.Domain.Comments.Commands.CreatePost;
using SocialNetwork.Application.Domain.Comments.Commands.LikeComment;
using SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;
using SocialNetwork.Application.Domain.Posts.Commands.AddLike;
using System.Security.Claims;

namespace SocialNetwork.API.MVC.Controllers;

public class CommentsController(IMediator mediator) : Controller
{
    public async Task<IActionResult> AddComment(
        Guid postId,
        string Content,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCommentCommand(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
            postId,
            Content);
        await mediator.Send(command, cancellationToken);
        
        return RedirectToAction("GetPostComments", "Posts", new { postId = postId });
    }

    public async Task<IActionResult> GetUserComments(
        string userId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserCommentsQuery(userId, page, pageSize);
        var comments = await mediator.Send(query, cancellationToken);

        var commentModels = comments.Data.Select(c => new CommentViewModel(
            c.CommentId,
            c.Content,
            c.LikeCount,
            c.CreationTime,
            new UserViewModel(
                c.User.UserName,
                c.User.ProfilePicturePath))
        );

        return View(commentModels);
    }

    public async Task<IActionResult> LikeComment(
        Guid commentId,
        Guid postId,
        CancellationToken cancellationToken = default)
    {
        var command = new LikeCommentCommand(
            commentId,
            true);

        await mediator.Send(command, cancellationToken);
        return RedirectToAction("GetPostComments", "Posts", new { postId = postId });
    }
}

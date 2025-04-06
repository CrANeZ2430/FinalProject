using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Domain.Comments.Commands.CreatePost;
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
}

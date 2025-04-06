using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models.Dtos;
using SocialNetwork.API.MVC.Models.ViewModels;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Application.Domain.Posts.Commands.AddLike;
using SocialNetwork.Application.Domain.Posts.Commands.CreatePost;
using SocialNetwork.Application.Domain.Posts.Queries.GetPosts;
using System.Security.Claims;

namespace SocialNetwork.API.MVC.Controllers;

public class PostsController(
    IMediator mediator) 
    : Controller
{
    public async Task<IActionResult> AddPost(
        AddPostFormViewModel model,
        CancellationToken cancellationToken = default)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var command = new CreatePostCommand(
            model.Title,
            model.Content,
            null,
            userId);

        await mediator.Send(command, cancellationToken);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> GetPosts(
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPostsQuery(page, pageSize);
        var posts = await mediator.Send(query, cancellationToken);
        var models = posts.Data.Select(p => new PostViewModel(
            p.PostId,
            p.Title,
            p.Content,
            //p.CommentCount,
            p.LikeCount,
            p.CreationTime,
            new UserViewModel(
                p.User.UserName,
                p.User.ProfileImagePath)
            ));

        return View(models);
    }

    public async Task<IActionResult> LikePost(
        Guid postId,
        CancellationToken cancellationToken = default)
    {
        var command = new LikePostCommand(
            postId,
            true);

        await mediator.Send(command, cancellationToken);
        return RedirectToAction("GetPosts", "Posts");
    }

    public async Task<IActionResult> GetPostComments(
        Guid postId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPostCommentsQuery(postId, page, pageSize);
        var comments = await mediator.Send(query, cancellationToken);
        var post = comments.Data;
        var models = new PostCommentsDto(
            new PostViewModel(
                post.PostId,
                post.Title,
                post.Content,
                post.LikeCount,
                post.CreationTime,
                new UserViewModel(
                    post.User.UserName,
                    post.User.ProfilePicturePath)),
            post.Comments.Select(c => new CommentViewModel(
                c.CommentId,
                c.Content,
                c.LikeCount,
                c.CreationTime,
                new UserViewModel(
                    c.User.UserName,
                    c.User.ProfilePicturePath))
            ).ToArray());

        return View(models);
    }
}

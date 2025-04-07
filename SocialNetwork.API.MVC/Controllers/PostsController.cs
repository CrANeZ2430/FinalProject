using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models.Dtos;
using SocialNetwork.API.MVC.Models.ViewModels;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Application.Domain.Posts.Commands.AddLike;
using SocialNetwork.Application.Domain.Posts.Commands.CreatePost;
using SocialNetwork.Application.Domain.Posts.Commands.DeletePost;
using SocialNetwork.Application.Domain.Posts.Queries.GetPosts;
using SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;
using System.Security.Claims;

namespace SocialNetwork.API.MVC.Controllers;

public class PostsController(
    IMediator mediator)
    : Controller
{
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
            p.LikeCount,
            p.CommentCount,
            p.CreationTime,
            p.User != null
                    ? new UserViewModel(
                        p.User.UserId,
                        p.User.UserName,
                        p.User.ProfilePicturePath)
                    : null
            ));

        return View(models);
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
            //posts
            new CommentsPostViewModel(
                post.PostId,
                post.Title,
                post.Content,
                post.LikeCount,
                post.CreationTime,
                post.User != null
                    ? new UserViewModel(
                        post.User.UserId,
                        post.User.UserName,
                        post.User.ProfilePicturePath)
                    : null),
            //comments
            post.Comments.Select(c => new CommentViewModel(
                c.CommentId,
                c.Content,
                c.LikeCount,
                c.CreationTime,
                c.User != null
                    ? new UserViewModel(
                        c.User.UserId,
                        c.User.UserName,
                        c.User.ProfilePicturePath)
                    : null)
            ).ToArray());

        return View(models);
    }

    public async Task<IActionResult> GetUserPosts(
        string userId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserPostsQuery(page, pageSize, userId);
        var posts = await mediator.Send(query, cancellationToken);

        var postModels = posts.Data.Select(p => new PostViewModel(
            p.PostId,
            p.Title,
            p.Content,
            p.LikeCount,
            p.CommentCount,
            p.CreationTime,
            new UserViewModel(
                p.User.UserId,
                p.User.UserName,
                p.User.ProfilePicturePath)
            ));

        return View(postModels);
    }

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

    public async Task<IActionResult> LikePost(
        Guid postId,
        string redirectUrl,
        CancellationToken cancellationToken = default)
    {
        var command = new LikePostCommand(
            postId,
            true);

        await mediator.Send(command, cancellationToken);
        return RedirectToAction(redirectUrl, new { postId = postId });
    }

    public async Task<IActionResult> DeletePost(
        Guid postId,
        string redirectUrl,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePostCommand(
            postId);

        await mediator.Send(command, cancellationToken);
        return RedirectToAction("GetPosts", "Posts");
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.MVC.Models.Dtos;
using SocialNetwork.API.MVC.Models.ViewModels;
using SocialNetwork.API.MVC.Services;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Application.Domain.Posts.Commands.AddLike;
using SocialNetwork.Application.Domain.Posts.Commands.CreatePost;
using SocialNetwork.Application.Domain.Posts.Commands.DeletePost;
using SocialNetwork.Application.Domain.Posts.Queries.GetPosts;
using SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;
using System.Security.Claims;

namespace SocialNetwork.API.MVC.Controllers;

public class PostsController(
    IMediator mediator,
    IPhotoService photoService)
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
            p.ImagePath,
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
                post.ImagePath,
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
            p.ImagePath,
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

    [Authorize]
    public async Task<IActionResult> AddPost(
        AddPostFormViewModel model,
        CancellationToken cancellationToken = default)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var images = new List<string>();
        if (model.Images != null && model.Images.Length > 0)
        {
            foreach (var image in model.Images)
            {
                var imagePath = photoService.AddPhotoAsync(image).Result;
                images.Add(imagePath.SecureUrl.ToString());
            }
        }

        var command = new CreatePostCommand(
            model.Title,
            model.Content,
            images.ToArray(),
            userId);

        await mediator.Send(command, cancellationToken);

        return RedirectToAction("GetPosts", "Posts");
    }

    [Authorize]
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

    [Authorize]
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

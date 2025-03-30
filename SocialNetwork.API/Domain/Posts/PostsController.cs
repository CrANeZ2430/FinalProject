﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Common.Constants;
using SocialNetwork.Application.Domain.Posts.Commands.AddLike;
using SocialNetwork.Application.Domain.Posts.Commands.CreatePost;
using SocialNetwork.Application.Domain.Posts.Commands.DeletePost;
using SocialNetwork.Application.Domain.Posts.Queries.GetPosts;
using SocialNetwork.Application.Domain.Posts.Queries.GetUserPosts;
using SocialNetwork.Core.Domain.Posts.Data;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Domain.Posts;

[Route(Routes.Posts)]
public class PostsController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPosts(
        [FromQuery][Required] int page = 1,
        [FromQuery][Required] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPostsQuery(
            page,
            pageSize);

        var posts = await mediator.Send(query, cancellationToken);
        return Ok(posts);
    }

    [HttpGet("from-user/{userId}")]
    public async Task<IActionResult> GetPost(
        [FromRoute][Required] Guid userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserPostsQuery(
            page,
            pageSize,
            userId);

        var post = await mediator.Send(query, cancellationToken);

        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CretePost(
        [FromQuery] CreatePostData data,
        CancellationToken cancellationToken = default)
    {
        var command = new CreatePostCommand(
            data.UserId,
            data.Title,
            data.Content,
            data.ImagePath);

        var id = await mediator.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(
        [FromRoute][Required] Guid postId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePostCommand(
            postId);

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut("add-like/{postId}")]
    public async Task<IActionResult> LikePost(
        [FromRoute][Required] Guid postId,
        [FromQuery][Required] bool isLike,
        CancellationToken cancellationToken = default)
    {
        var command = new LikePostCommand(postId, isLike);
        
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}

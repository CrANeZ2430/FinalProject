using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Common.Constants;
using SocialNetwork.API.Domain.Comments.Records;
using SocialNetwork.Application.Domain.Comments.Commands.CreatePost;
using SocialNetwork.Application.Domain.Comments.Commands.DeleteComment;
using SocialNetwork.Application.Domain.Comments.Commands.LikeComment;
using SocialNetwork.Application.Domain.Comments.Commands.UpdateComment;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
using SocialNetwork.Application.Domain.Comments.Queries.GetUserComments;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Domain.Comments;

[Route(Routes.Comments)]
public class CommentsController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("from-post/{postId}")]
    public async Task<IActionResult> GetPostComments(
        [FromRoute][Required] Guid postId,
        [FromQuery][Required] int page = 1,
        [FromQuery][Required] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPostCommentsQuery(
            postId,
            page,
            pageSize);

        var comments = await mediator.Send(query, cancellationToken);

        return Ok(comments);
    }

    [HttpGet("from-user/{userId}")]
    public async Task<IActionResult> GetUserComments(
        [FromRoute][Required] Guid userId,
        [FromQuery][Required] int page = 1,
        [FromQuery][Required] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetUserCommentsQuery(
            userId,
            page,
            pageSize);

        var comments = await mediator.Send(query, cancellationToken);

        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(
        [FromQuery] CreateCommentRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCommentCommand(
            request.UserId,
            request.PostId,
            request.Content);

        var id = await mediator.Send(command, cancellationToken);

        return Ok(id);
    }

    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateComment(
        [FromRoute][Required] Guid commentId,
        [FromQuery][Required] UpdateCommentRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCommentCommand(
            commentId,
            request.Content);

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(
        [FromRoute][Required] Guid commentId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCommentCommand(
            commentId);

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPut("add-like/{commentId}")]
    public async Task<IActionResult> LikeComment(
        [FromRoute][Required] Guid commentId,
        [FromQuery][Required] bool isLike,
        CancellationToken cancellationToken = default)
    {
        var command = new LikeCommentCommand(
            commentId,
            isLike);

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Common.Constants;
using SocialNetwork.API.Domain.Comments.Records;
using SocialNetwork.Application.Domain.Comments.Commands.CreatePost;
using SocialNetwork.Application.Domain.Comments.Queries.GetPostComments;
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

        var comments = await mediator.Send(query);

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

        var id = await mediator.Send(command);
        return Ok(id);
    }
}

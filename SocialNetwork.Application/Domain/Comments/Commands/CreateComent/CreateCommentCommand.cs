using MediatR;

namespace SocialNetwork.Application.Domain.Comments.Commands.CreatePost;

public record CreateCommentCommand(
    Guid UserId,
    Guid PostId,
    string Content)
    : IRequest<Guid>;
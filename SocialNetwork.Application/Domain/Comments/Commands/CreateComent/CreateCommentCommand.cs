using MediatR;

namespace SocialNetwork.Application.Domain.Comments.Commands.CreatePost;

public record CreateCommentCommand(
    string UserId,
    Guid PostId,
    string Content)
    : IRequest<Guid>;
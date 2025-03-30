using MediatR;

namespace SocialNetwork.Application.Domain.Comments.Commands.UpdateComment;

public record UpdateCommentCommand(
    Guid CommentId,
    string Content)
    : IRequest;

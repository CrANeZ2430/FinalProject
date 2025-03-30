using MediatR;

namespace SocialNetwork.Application.Domain.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(
    Guid CommentId)
    : IRequest;

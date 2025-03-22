using MediatR;

namespace SocialNetwork.Application.Domain.Comments.Commands.LikeComment;

public record LikeCommentCommand(
    Guid CommentId,
    bool IsLike)
    : IRequest;

using MediatR;

namespace SocialNetwork.Application.Domain.Posts.Commands.AddLike;

public record LikePostCommand(
    Guid PostId,
    bool IsLike)
    : IRequest;

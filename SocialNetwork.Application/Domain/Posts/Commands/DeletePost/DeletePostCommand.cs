using MediatR;

namespace SocialNetwork.Application.Domain.Posts.Commands.DeletePost;

public record DeletePostCommand(
    Guid PostId)
    : IRequest;

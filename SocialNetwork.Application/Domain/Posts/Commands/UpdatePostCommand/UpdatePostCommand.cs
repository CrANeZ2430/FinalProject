using MediatR;

namespace SocialNetwork.Application.Domain.Posts.Commands.UpdatePostCommand;

public record UpdatePostCommand(
    Guid PostId,
    string Title,
    string Content,
    string[]? ImagePath)
    : IRequest;

using MediatR;

namespace SocialNetwork.Application.Domain.Posts.Commands.CreatePost;

public record CreatePostCommand(
    Guid UserId,
    string Title,
    string Content,
    string? ImagePath)
    : IRequest<Guid>;

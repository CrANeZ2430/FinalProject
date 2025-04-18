﻿using MediatR;

namespace SocialNetwork.Application.Domain.Posts.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Content,
    string[]? ImagePath,
    string UserId)
    : IRequest<Guid>;

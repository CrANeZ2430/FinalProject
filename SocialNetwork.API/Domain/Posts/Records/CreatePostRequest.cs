﻿namespace SocialNetwork.API.Domain.Posts.Records;

public record CreatePostRequest(
    string Title,
    string Content,
    string[]? ImagePath,
    Guid UserId);

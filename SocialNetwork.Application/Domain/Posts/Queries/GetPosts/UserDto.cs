﻿namespace SocialNetwork.Application.Domain.Posts.Queries.GetPosts;

public record UserDto(
    string UserName,
    string Email,
    string PasswordHash);

﻿namespace SocialNetwork.Core.Domain.Users.Data;

public record CreateUserData(
    string UserName,
    string Email,
    string Password,
    string ProfilePicturePath,
    string Bio);

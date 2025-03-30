﻿namespace SocialNetwork.Core.Domain.Users.Data;

public record UpdateUserData(
    string UserName,
    string Email,
    string PasswordHash,
    string ProfilePicturePath,
    string Bio);

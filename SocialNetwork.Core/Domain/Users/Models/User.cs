﻿using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Data;
using SocialNetwork.Core.Domain.Users.Validators;

namespace SocialNetwork.Core.Domain.Users.Models;

public class User : Entity
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();

    private User() { }

    public User(
        Guid userId, 
        string userName, 
        string email, 
        string passwordHash, 
        string profilePicturePath, 
        string? bio, 
        DateTime creationTime)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        ProfilePicturePath = profilePicturePath;
        Bio = bio;
        CreationTime = creationTime;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string ProfilePicturePath { get; private set; }
    public string? Bio { get; private set; }
    public DateTime CreationTime { get; private set; }
    public IReadOnlyCollection<Post> Posts => _posts;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static async Task<User> Create(
        CreateUserData data,
        IEmailMustBeUniqueChecker emailChecker,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new CreateUserDataValidator(emailChecker), data, cancellationToken);

        return new User(
            Guid.NewGuid(),
            data.UserName,
            data.Email,
            data.PasswordHash,
            data.ProfilePicturePath,
            data.Bio,
            DateTime.UtcNow);
    }
}

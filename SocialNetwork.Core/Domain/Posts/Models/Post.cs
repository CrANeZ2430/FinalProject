﻿using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Posts.Models;

public class Post
{
    private readonly User? _user;
    private readonly List<Comment> _comments;

    private Post() { }

    public Post(
        Guid postId,
        string title, 
        string content,
        string[]? imagePath,
        DateTime creationTime, 
        DateTime updateTime,
        Guid? userId)
    {
        PostId = postId;
        UserId = userId;
        Title = title;
        Content = content;
        ImagePath = imagePath;
        LikeCount = 0;
        CreationTime = creationTime;
        UpdateTime = updateTime;
    }

    public Guid PostId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string[]? ImagePath { get; private set; }
    public int LikeCount { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public Guid? UserId { get; private set; }
    public User? User => _user;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static Post Create(CreatePostData data)
    {
        return new Post(
            Guid.NewGuid(),
            data.Title,
            data.Content,
            data.ImagePath,
            DateTime.UtcNow,
            DateTime.UtcNow,
            data.UserId);
    }

    public void LikePost(bool isLike)
    {
        if(isLike) LikeCount++;
        else LikeCount--;
    }
}

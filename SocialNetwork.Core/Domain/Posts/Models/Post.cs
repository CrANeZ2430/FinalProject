using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Models;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Posts.Validators;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Posts.Models;

public class Post : Entity
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
        string? userId)
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
    public DateTime CreationTime { get; init; }
    public DateTime UpdateTime { get; private set; }
    public string? UserId { get; private set; }
    public User? User => _user;
    public IReadOnlyCollection<Comment> Comments => _comments;

    public static async Task<Post> Create(
        CreatePostData data,
        IUsersRepository usersRepository)
    {
        await ValidateAsync(new CreatePostDataValidator(usersRepository), data);

        return new Post(
            Guid.NewGuid(),
            data.Title,
            data.Content,
            data.ImagePath,
            DateTime.UtcNow,
            DateTime.UtcNow,
            data.UserId);
    }

    public async Task Update(
        UpdatePostData data,
        IUsersRepository usersRepository)
    {
        await ValidateAsync(new UpdatePostDataValidator(usersRepository), data);

        Title = data.Title;
        Content = data.Content;
        ImagePath = data.ImagePath;
        UpdateTime = DateTime.UtcNow;
    }

    public void LikePost(bool isLike)
    {
        if(isLike) LikeCount++;
        else LikeCount--;
    }
}

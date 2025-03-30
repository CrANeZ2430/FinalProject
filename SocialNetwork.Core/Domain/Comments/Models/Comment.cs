using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Comments.Validator;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Comments.Models;

public class Comment : Entity
{
    private readonly Post _post;
    private readonly User? _user;

    private Comment() { }

    public Comment(
        Guid commentId,
        string content,
        DateTime creationTime,
        DateTime updateTime,
        Guid? userId,
        Guid postId)
    {
        CommentId = commentId;
        UserId = userId;
        PostId = postId;
        Content = content;
        LikeCount = 0;
        CreationTime = creationTime;
        UpdateTime = updateTime;
    }

    public Guid CommentId { get; private set; }
    public string Content { get; private set; }
    public int LikeCount { get; private set; }
    public DateTime CreationTime { get; init; }
    public DateTime UpdateTime { get; private set; }
    public Guid? UserId { get; private set; }
    public Guid PostId { get; private set; }
    public Post Post => _post;
    public User? User => _user;

    public static async Task<Comment> Create(
        CreateCommentData data,
        IUsersRepository usersRepository,
        IPostsRepository postsRepository)
    {
        await ValidateAsync(new CreateCommentDataValidator(
            usersRepository, 
            postsRepository), 
            data);

        return new Comment(
            Guid.NewGuid(),
            data.Content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            data.UserId,
            data.PostId);
    }

    public async Task Update(
        UpdateCommentData data,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new UpdateCommentDataValidator(),
            data);

        Content = data.Content;
        UpdateTime = DateTime.UtcNow;
    }

    public void LikeComment(bool isLike)
    {
        if(isLike) LikeCount++;
        else LikeCount--;
    }
}

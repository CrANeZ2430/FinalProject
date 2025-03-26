using SocialNetwork.Core.Domain.Comments.Data;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Comments.Models;

public class Comment
{
    private readonly Post _post;
    private readonly User? _user;

    private Comment() { }

    public Comment(
        Guid commentId,
        Guid? userId,
        Guid postId,
        string content,
        DateTime creationTime,
        DateTime updateTime)
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
    public Guid? UserId { get; private set; }
    public Guid PostId { get; private set; }
    public string Content { get; private set; }
    public int LikeCount { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime UpdateTime { get; private set; }
    public Post Post => _post;
    public User? User => _user;

    public static Comment Create(CreateCommentData data)
    {
        return new Comment(
            Guid.NewGuid(),
            data.UserId,
            data.PostId,
            data.Content,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    public void LikeComment(bool isLike)
    {
        if(isLike) LikeCount++;
        else LikeCount--;
    }
}

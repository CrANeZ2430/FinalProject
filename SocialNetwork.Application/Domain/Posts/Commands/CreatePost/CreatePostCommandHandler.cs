using MediatR;
using SocialNetwork.Core.Common.DbContext;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Posts.Models;

namespace SocialNetwork.Application.Domain.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(
    IPostsRepository postsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePostCommand, Guid>
{
    public async Task<Guid> Handle(
        CreatePostCommand command, 
        CancellationToken cancellationToken)
    {
        var data = new CreatePostData(
            command.UserId,
            command.Title,
            command.Content,
            command.ImagePath);

        var post = Post.Create(data);
        postsRepository.Add(post);
        await unitOfWork.SaveChangesAsync();

        return post.PostId;
    }
}

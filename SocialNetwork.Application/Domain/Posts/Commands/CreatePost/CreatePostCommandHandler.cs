using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Posts.Models;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(
    IUsersRepository usersRepository,
    IPostsRepository postsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePostCommand, Guid>
{
    public async Task<Guid> Handle(
        CreatePostCommand command, 
        CancellationToken cancellationToken = default)
    {
        var data = new CreatePostData(
            command.Title,
            command.Content,
            command.ImagePath,
            command.UserId);

        var post = await Post.Create(data, usersRepository);
        postsRepository.Add(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return post.PostId;
    }
}

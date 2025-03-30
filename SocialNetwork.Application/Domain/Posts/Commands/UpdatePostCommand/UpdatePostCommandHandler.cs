using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Posts.Data;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Posts.Commands.UpdatePostCommand;

public class UpdatePostCommandHandler(
    IUsersRepository usersRepository,
    IPostsRepository postsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(command.PostId, cancellationToken);

        var data = new UpdatePostData(
            command.Title,
            command.Content,
            command.ImagePath);

        await post.Update(data, usersRepository);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

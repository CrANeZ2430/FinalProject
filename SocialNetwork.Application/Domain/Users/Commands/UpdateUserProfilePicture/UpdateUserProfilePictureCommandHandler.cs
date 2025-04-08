using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Data;

namespace SocialNetwork.Application.Domain.Users.Commands.UpdateUserProfilePicture;

public class UpdateUserProfilePictureCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserProfilePictureCommand>
{
    public async Task Handle(
        UpdateUserProfilePictureCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await usersRepository.GetById(command.UserId, cancellationToken);

        var data = new UpdateUserProfilePictureData(
            command.ProfilePicturePath);
        await user.UpdateProfilePicture(data, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

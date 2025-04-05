using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Data;

namespace SocialNetwork.Application.Domain.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IEmailMustBeUniqueChecker emailChecker,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(
        UpdateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetById(command.UserId, cancellationToken);

        var data = new UpdateUserData(
            command.UserName,
            command.Email,
            command.ProfilePicturePath,
            command.Bio);

        await user.Update(data, emailChecker);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

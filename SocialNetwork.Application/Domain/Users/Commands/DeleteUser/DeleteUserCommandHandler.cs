using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(
        DeleteUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user = await usersRepository.GetById(command.UserId);

        usersRepository.Remove(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

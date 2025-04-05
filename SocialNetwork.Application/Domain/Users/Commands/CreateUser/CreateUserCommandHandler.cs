using MediatR;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Data;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Application.Domain.Users.Commands.CreateUser;

internal class CreateUserCommandHandler(
    IUsersRepository userRepository,
    IUnitOfWork unitOfWork,
    IEmailMustBeUniqueChecker emailChecker) 
    : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(
        CreateUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var data = new CreateUserData(
            command.UserId,
            command.UserName,
            command.Email,
            command.ProfilePicturePath,
            command.Bio);

        var user = await User.Create(data, emailChecker);
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

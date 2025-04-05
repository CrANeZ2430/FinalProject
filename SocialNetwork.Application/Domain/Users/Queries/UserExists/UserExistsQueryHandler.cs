using MediatR;
using SocialNetwork.Core.Domain.Users.Common;

namespace SocialNetwork.Application.Domain.Users.Queries.UserExists;

public class UserExistsQueryHandler(
    IUsersRepository usersRepository)
    : IRequestHandler<UserExistsQuery, bool>
{
    public async Task<bool> Handle(
        UserExistsQuery query, 
        CancellationToken cancellationToken = default)
    {
        return await usersRepository.UserExists(query.UserId, cancellationToken);
    }
}

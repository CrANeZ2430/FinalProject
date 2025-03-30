using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Core.Domain.Users.Models;
using SocialNetwork.Core.Exceptions;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Domain.Users.Common;

internal class UsersRepository(SocialNetworkDbContext dbContext) : IUsersRepository
{
    public void Add(User user)
    {
        dbContext.Users.Add(user);
    }

    public void Remove(User user)
    {
        dbContext.Remove(user);
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users
                    .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken)
                    ?? throw new NotFoundException($"{nameof(User)} cannot be found.");
    }

    public async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users
                    .AnyAsync(x => x.UserId == userId, cancellationToken);
    }
}

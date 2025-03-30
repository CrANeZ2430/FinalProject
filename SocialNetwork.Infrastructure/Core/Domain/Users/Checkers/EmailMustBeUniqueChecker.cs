using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Domain.Users.Checkers;

public class EmailMustBeUniqueChecker(
    SocialNetworkDbContext dbContext)
    : IEmailMustBeUniqueChecker
{
    public async Task<bool> IsUnique(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.AllAsync(x => x.Email != email, cancellationToken);
    }
}

using SocialNetwork.Core.Common;
using SocialNetwork.Persistence.SocialNetworkDb;

namespace SocialNetwork.Infrastructure.Core.Common.UnitOfWork;

internal class UnitOfWork(SocialNetworkDbContext dbContext) : IUnitOfWork
{
    public async  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

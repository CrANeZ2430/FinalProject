using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Common.DbContext;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

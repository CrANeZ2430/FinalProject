using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Users.Common;

public interface IUserRepository
{
    void Add(User user);
    void Remove(User user);
    Task<User> GetById(Guid id, CancellationToken cancellationToken = default);
}

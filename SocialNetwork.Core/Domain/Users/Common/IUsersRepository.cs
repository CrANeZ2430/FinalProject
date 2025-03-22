using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Users.Common;

public interface IUsersRepository
{
    void Add(User user);
    void Remove(User user);
    Task<User> GetById(Guid userId, CancellationToken cancellationToken = default);
}

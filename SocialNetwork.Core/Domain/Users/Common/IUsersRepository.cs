using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Users.Common;

public interface IUsersRepository
{
    void Add(User user);
    void Remove(User user);
    Task<User> GetById(string userId, CancellationToken cancellationToken = default);
    Task<bool> UserExists(string userId, CancellationToken cancellationToken = default);
}

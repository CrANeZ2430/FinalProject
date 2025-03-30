namespace SocialNetwork.Core.Domain.Users.Checkers;

public interface IEmailMustBeUniqueChecker
{
    Task<bool> IsUnique(string email, CancellationToken cancellationToken = default);
}

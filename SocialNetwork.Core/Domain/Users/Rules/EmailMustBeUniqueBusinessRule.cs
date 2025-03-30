using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Core.Domain.Users.Rules;

public class EmailMustBeUniqueBusinessRule(
    string email,
    IEmailMustBeUniqueChecker checker)
    : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await checker.IsUnique(email, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isBelongs)
    {
        if (isBelongs) return RuleResult.Success();
        return RuleResult.Failed($"{nameof(User)}'s {nameof(User.Email)} must be unique.");
    }
}

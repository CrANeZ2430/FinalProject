using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Common.DbContext;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Infrastructure.Core.Common.UnitOfWork;
using SocialNetwork.Infrastructure.Core.Posts.Common;
using SocialNetwork.Infrastructure.Core.Users.Common;
using System.Reflection;

namespace SocialNetwork.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UsersRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
    }
}

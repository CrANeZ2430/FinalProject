using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Common;
using SocialNetwork.Core.Domain.Comments.Common;
using SocialNetwork.Core.Domain.Posts.Common;
using SocialNetwork.Core.Domain.Users.Checkers;
using SocialNetwork.Core.Domain.Users.Common;
using SocialNetwork.Infrastructure.Core.Common.UnitOfWork;
using SocialNetwork.Infrastructure.Core.Domain.Comments.Common;
using SocialNetwork.Infrastructure.Core.Domain.Posts.Common;
using SocialNetwork.Infrastructure.Core.Domain.Users.Checkers;
using SocialNetwork.Infrastructure.Core.Domain.Users.Common;
using SocialNetwork.Infrastructure.Middleware;
using System.Reflection;

namespace SocialNetwork.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();

        services.AddScoped<IEmailMustBeUniqueChecker, EmailMustBeUniqueChecker>();

        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddTransient<ExceptionHandlerMiddleware>();
    }
}

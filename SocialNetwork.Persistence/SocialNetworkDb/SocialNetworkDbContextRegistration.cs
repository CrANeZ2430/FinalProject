using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Persistence.SocialNetworkDb;

public static class SocialNetworkDbContextRegistration
{
    public static void RegisterSocialNetworkDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SocialNetworkDb");

        services.AddDbContext<SocialNetworkDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        SocialNetworkDbContext.SocialNetworkMigrationHistory,
                        SocialNetworkDbContext.SocialNetworkDbSchema);
                });
        });
    }
}

using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb;

public class SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options) : DbContext(options)
{
    public static string SocialNetworkDbSchema = "socialNetwork";
    public static string SocialNetworkMigrationHistory = "__SocialNetworkMigrationHistory";

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SocialNetworkDbSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialNetworkDbContext).Assembly);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityConfigurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.ProfilePicturePath)
            .HasMaxLength(255);

        builder.Property(x => x.Bio)
            .HasMaxLength(2000);

        builder.Property(x => x.CreationTime)
            .IsRequired();

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(User.Posts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

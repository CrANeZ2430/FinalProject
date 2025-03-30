using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Users.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityConfigurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .HasColumnOrder(1);

        builder.Property(x => x.UserName)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(2);

        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(3);
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(64)
            .IsRequired()
            .HasColumnOrder(4);

        builder.Property(x => x.ProfilePicturePath)
            .HasMaxLength(255)
            .IsRequired()
            .HasColumnOrder(5);

        builder.Property(x => x.Bio)
            .HasMaxLength(300)
            .IsRequired(false)
            .HasColumnOrder(6);

        builder.Property(x => x.CreationTime)
            .IsRequired()
            .HasColumnOrder(7);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.Metadata
            .FindNavigation(nameof(User.Posts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(User.Comments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

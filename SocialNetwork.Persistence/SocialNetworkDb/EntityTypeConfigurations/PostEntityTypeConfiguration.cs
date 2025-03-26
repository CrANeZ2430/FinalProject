using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Posts.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityConfigurations;

internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.PostId);

        builder.Property(x => x.UserId)
            .IsRequired(false);

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.ImagePath)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(x => x.LikeCount);

        builder.Property(x => x.CreationTime)
            .IsRequired();

        builder.Property(x => x.UpdateTime)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.Metadata
            .FindNavigation(nameof(Post.User))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Post.Comments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

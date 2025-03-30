using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Posts.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityConfigurations;

internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.PostId);

        builder.Property(x => x.PostId)
            .HasColumnOrder(1);

        builder.Property(x => x.UserId)
            .IsRequired(false)
            .HasColumnOrder(2);

        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property(x => x.Content)
            .HasMaxLength(500)
            .IsRequired()
            .HasColumnOrder(4);

        builder.Property(x => x.ImagePath)
            .HasMaxLength(255)
            .IsRequired(false)
            .HasColumnOrder(5);

        builder.Property(x => x.LikeCount);

        builder.Property(x => x.CreationTime)
            .IsRequired()
            .HasColumnOrder(6);

        builder.Property(x => x.UpdateTime)
            .IsRequired()
            .HasColumnOrder(7);

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

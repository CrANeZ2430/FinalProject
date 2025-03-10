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
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.ImagePath)
            .HasMaxLength(255);

        builder.Property(x => x.CreationTime)
            .IsRequired();

        builder.Property(x => x.UpdateTime)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(Post.User))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

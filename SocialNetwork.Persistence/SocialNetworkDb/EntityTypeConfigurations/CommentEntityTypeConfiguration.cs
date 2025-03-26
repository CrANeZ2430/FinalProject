using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Comments.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityTypeConfigurations;

internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.CommentId);

        builder.Property(x => x.UserId)
            .IsRequired(false);

        builder.Property(x => x.PostId)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.LikeCount);

        builder.Property(x => x.CreationTime)
            .IsRequired();

        builder.Property(x => x.UpdateTime)
            .IsRequired();

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.Metadata
            .FindNavigation(nameof(Comment.User))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Comment.Post))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

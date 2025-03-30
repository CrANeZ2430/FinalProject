using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Core.Domain.Comments.Models;

namespace SocialNetwork.Persistence.SocialNetworkDb.EntityTypeConfigurations;

internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.CommentId);

        builder.Property(x => x.CommentId)
            .HasColumnOrder(1);

        builder.Property(x => x.UserId)
            .IsRequired(false)
            .HasColumnOrder(2);

        builder.Property(x => x.PostId)
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property(x => x.Content)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnOrder(4);

        builder.Property(x => x.LikeCount)
            .IsRequired()
            .HasColumnOrder(5);

        builder.Property(x => x.CreationTime)
            .IsRequired()
            .HasColumnOrder(6);

        builder.Property(x => x.UpdateTime)
            .IsRequired()
            .HasColumnOrder(7);

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

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialNetwork.Persistence.SocialNetworkDb;

#nullable disable

namespace SocialNetwork.Persistence.SocialNetworkDb.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20250404214147_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("socialNetwork")
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialNetwork.Core.Domain.Comments.Models.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LikeCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", "socialNetwork");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Posts.Models.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string[]>("ImagePath")
                        .HasMaxLength(255)
                        .HasColumnType("text[]");

                    b.Property<int>("LikeCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts", "socialNetwork");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Users.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(1);

                    b.Property<string>("Bio")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(6);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(3);

                    b.Property<string>("ProfilePicturePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnOrder(4);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(2);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", "socialNetwork");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Comments.Models.Comment", b =>
                {
                    b.HasOne("SocialNetwork.Core.Domain.Posts.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SocialNetwork.Core.Domain.Users.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Posts.Models.Post", b =>
                {
                    b.HasOne("SocialNetwork.Core.Domain.Users.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Posts.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SocialNetwork.Core.Domain.Users.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using DomainData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace News.DomainData.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191117015804_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DomainData.Models.CommentLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CommentId")
                        .HasColumnType("integer");

                    b.Property<string>("DataId")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("character varying(890)")
                        .HasMaxLength(890);

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("Type");

                    b.HasIndex("Url");

                    b.ToTable("CommentsLinks");
                });

            modelBuilder.Entity("DomainData.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("text");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("AuthorName")
                        .HasColumnType("text");

                    b.Property<string>("BannedBy")
                        .HasColumnType("text");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedUTC")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Domain")
                        .HasColumnType("text");

                    b.Property<int>("Downvotes")
                        .HasColumnType("integer");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<int>("Gilded")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsLiked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsNSFW")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsRemoved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSaved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSelfPost")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSpoiler")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStickied")
                        .HasColumnType("boolean");

                    b.Property<string>("Kind")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MyVote")
                        .HasColumnType("integer");

                    b.Property<string>("Permalink")
                        .HasColumnType("text");

                    b.Property<TimeSpan?>("ProcessedTime")
                        .HasColumnType("interval");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("ReportCount")
                        .HasColumnType("integer");

                    b.Property<string>("SelfText")
                        .HasColumnType("text");

                    b.Property<string>("SelfTextHtml")
                        .HasColumnType("text");

                    b.Property<string>("Shortlink")
                        .HasColumnType("text");

                    b.Property<string>("SubRedditId")
                        .HasColumnType("text");

                    b.Property<int?>("TagId")
                        .HasColumnType("integer");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Upvotes")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SubRedditId");

                    b.HasIndex("TagId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DomainData.Models.PostLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DataId")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("PostId1")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("character varying(890)")
                        .HasMaxLength(890);

                    b.HasKey("Id");

                    b.HasIndex("PostId1");

                    b.HasIndex("Type");

                    b.HasIndex("Url");

                    b.ToTable("PostsLinks");
                });

            modelBuilder.Entity("DomainData.Models.PostTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("DomainData.Models.Setting", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("ValueInt")
                        .HasColumnType("integer");

                    b.Property<string>("ValueString")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Name");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("DomainData.Models.SubReddit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("CakeDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<int?>("Members")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SubReddits");
                });

            modelBuilder.Entity("DomainData.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CakeDay")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("Karma")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DomainData.Models.UserComment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsPostAuthor")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("integer");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("PostId1")
                        .HasColumnType("text");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PostId1");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DomainData.Models.UserLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DataId")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("character varying(890)")
                        .HasMaxLength(890);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.HasIndex("Url");

                    b.HasIndex("UserId");

                    b.ToTable("UsersLinks");
                });

            modelBuilder.Entity("DomainData.Models.CommentLink", b =>
                {
                    b.HasOne("DomainData.Models.UserComment", "Comment")
                        .WithMany("Links")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainData.Models.Post", b =>
                {
                    b.HasOne("DomainData.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId");

                    b.HasOne("DomainData.Models.SubReddit", "SubReddit")
                        .WithMany("Posts")
                        .HasForeignKey("SubRedditId");

                    b.HasOne("DomainData.Models.PostTag", "Tag")
                        .WithMany("Posts")
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("DomainData.Models.PostLink", b =>
                {
                    b.HasOne("DomainData.Models.Post", "Post")
                        .WithMany("PostLinks")
                        .HasForeignKey("PostId1");
                });

            modelBuilder.Entity("DomainData.Models.UserComment", b =>
                {
                    b.HasOne("DomainData.Models.UserComment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("DomainData.Models.Post", "Post")
                        .WithMany("UserComments")
                        .HasForeignKey("PostId1");

                    b.HasOne("DomainData.Models.User", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DomainData.Models.UserLink", b =>
                {
                    b.HasOne("DomainData.Models.User", "User")
                        .WithMany("UserReferences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DomainData.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DomainData
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<SubReddit> SubReddits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserComment> Comments { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<PostLink> PostsLinks { get; set; }
        public DbSet<CommentLink> CommentsLinks { get; set; }
        public DbSet<UserLink> UsersLinks { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Data Source=.");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<SubReddit>()
                .HasIndex(x=> x.Name);

            modelBuilder.Entity<PostTag>(u => u.HasIndex(x => x.Name));

            modelBuilder.Entity<PostLink>(u =>
            {
                u.HasIndex(x => x.Url);
                u.HasIndex(x => x.Type);
            });
            modelBuilder.Entity<CommentLink>(u =>
            {
                u.HasIndex(x => x.Url);
                u.HasIndex(x => x.Type);
            });
            modelBuilder.Entity<UserLink>(u =>
            {
                u.HasIndex(x => x.Url);
                u.HasIndex(x => x.Type);
            });


        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(IConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseNpgsql(connectionString);
            return new DatabaseContext(builder.Options);
        }
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = 
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../NewsUpdater/appsettings.json", optional: true)
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../NewsUpdater/appsettings.private.json", optional: true)
                .Build();
            return CreateDbContext(configuration);
        }
    }
}

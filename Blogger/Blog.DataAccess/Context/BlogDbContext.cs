using Blog.Entity;
using Blog.Entity.BlogEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.DataAccess.Context
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThemePreference> ThemePreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PostTag (many-to-many)
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            // ApplicationUser → Posts
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser → Comments
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post → Comments
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Post → Category
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // ThemePreference → ApplicationUser (1-1)
            modelBuilder.Entity<ThemePreference>()
                .HasOne(tp => tp.User)
                .WithMany()
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

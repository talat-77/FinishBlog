using Blog.Entity.BlogEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blog.DataAccess.Context
{
    public class BlogDbContext:DbContext
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PostTag (many-to-many)
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            // User → Posts
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ⚠️ Fix: User → Comments → Restrict (önceden SetNull idi, hata veriyordu)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 bu satır değiştirildi

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

            // ThemePreference → User (bire bir)
            modelBuilder.Entity<ThemePreference>()
                .HasOne(tp => tp.User)
                .WithMany()
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

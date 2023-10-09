using AsifBlog.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Data
{
    public class AsifBlogDbContext : DbContext
    {
        public AsifBlogDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComent> PostComents { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
    }
}

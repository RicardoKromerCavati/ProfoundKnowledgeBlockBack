using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Domain.Posts;
using ProfoundKnowledgeBlogBack.Domain.Users;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure;

public class ProfoundKnowledgeContext : DbContext
{
    public ProfoundKnowledgeContext(DbContextOptions<ProfoundKnowledgeContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>().HasKey(u => u.UserId);

        modelBuilder.Entity<Post>(post =>
        {
            post.HasKey(p => p.PostId);
            post
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}

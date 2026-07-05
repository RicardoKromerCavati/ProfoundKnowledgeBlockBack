using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Infrastructure.Posts;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure;

public class ProfoundKnowledgeContext : DbContext
{
    public ProfoundKnowledgeContext(DbContextOptions<ProfoundKnowledgeContext> options) : base(options)
    {
    }

    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbPost> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>().HasKey(u => u.UserId);
        modelBuilder.Entity<DbPost>().HasKey(p => p.PostId);
        base.OnModelCreating(modelBuilder);
    }
}

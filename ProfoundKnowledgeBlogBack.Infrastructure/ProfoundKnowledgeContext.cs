using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure;

public class ProfoundKnowledgeContext : DbContext
{
    public ProfoundKnowledgeContext(DbContextOptions<ProfoundKnowledgeContext> options) : base(options)
    {
    }

    public DbSet<DbUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>().HasKey(u => u.Id);
        base.OnModelCreating(modelBuilder);
    }
}

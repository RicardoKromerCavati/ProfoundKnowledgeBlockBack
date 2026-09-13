using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Users;

public class DbUser
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public DbUser(Guid id, string email, string passwordHash, string username)
    {
        UserId = id;
        Email = email;
        PasswordHash = passwordHash;
        Username = username;
    }

    public DbUser()
    {
        
    }

    public static DbUser Create(User user) => new(Guid.NewGuid(), user.Email, user.PasswordHash, user.Username);
}
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Users;

public class DbUser : IUser
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Username { get; set; }

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

    public static DbUser Create(IUser user) => new(Guid.NewGuid(), user.Email, user.PasswordHash, user.Username);
}
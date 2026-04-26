using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Users;

public class DbUser(Guid id, string email, string passwordHash, string username) : IUser
{
    public Guid Id { get; set; } = id;
    public string Email { get; set; } = email;
    public string PasswordHash { get; set; } = passwordHash;
    public string Username { get; set; } = username;

    public static DbUser Create(IUser user) => new(Guid.NewGuid(), user.Email, user.PasswordHash, user.Username);
}
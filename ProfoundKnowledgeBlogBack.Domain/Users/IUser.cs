namespace ProfoundKnowledgeBlogBack.Domain.Users;

public interface IUser
{
    string Email { get; set; }
    string PasswordHash { get; set; }
    string Username { get; set; }
}
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Users;

public class BusinessUser : IUser
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
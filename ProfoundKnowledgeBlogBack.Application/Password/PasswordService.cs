using ProfoundKnowledgeBlogBack.Domain.Password;

namespace ProfoundKnowledgeBlogBack.Application.Password;

public class PasswordService : IPasswordService
{
    public string Create(string rawPassword) => BCrypt.Net.BCrypt.HashPassword(rawPassword);

    public bool IsValid(string inputRawPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(inputRawPassword, password);
    }
}
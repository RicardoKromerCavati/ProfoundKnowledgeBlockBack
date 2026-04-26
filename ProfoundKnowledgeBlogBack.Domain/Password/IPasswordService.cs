namespace ProfoundKnowledgeBlogBack.Domain.Password;

public interface IPasswordService
{
    string Create(string rawPassword);
    bool IsValid(string inputRawPassword, string password);
}

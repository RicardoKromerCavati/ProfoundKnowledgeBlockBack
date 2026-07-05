namespace ProfoundKnowledgeBlogBack.Domain.Password;

public interface IPasswordService
{
    string Create(string rawPassword);
    bool IsStrong(string password);
    bool IsValid(string inputRawPassword, string password);
}

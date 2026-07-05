using ProfoundKnowledgeBlogBack.Domain.Password;
using System.Text.RegularExpressions;

namespace ProfoundKnowledgeBlogBack.Application.Password;

public class PasswordService : IPasswordService
{
    public string Create(string rawPassword) => BCrypt.Net.BCrypt.HashPassword(rawPassword);

    public bool IsValid(string inputRawPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(inputRawPassword, password);
    }

    public bool IsStrong(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }

        var hasUpperCase = Regex.IsMatch(password, "[A-Z]");
        var hasLowerCase = Regex.IsMatch(password, "[a-z]");
        var hasNumericChar = Regex.IsMatch(password, "[0-9]");
        var hasSpecialCharacters = Regex.IsMatch(password, "[^A-Za-z0-9]");
        var hasMinimumLength = password.Length >= 8;

        return hasUpperCase &&
               hasLowerCase &&
               hasNumericChar &&
               hasSpecialCharacters &&
               hasMinimumLength;
    }
}
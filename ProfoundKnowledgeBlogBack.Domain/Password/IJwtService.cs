using System.Security.Claims;

namespace ProfoundKnowledgeBlogBack.Domain.Password;

public interface IJwtService
{
    string CreateToken(string keyValue, string email, string username);
    ClaimsPrincipal ValidateToken(string keyStr, string authToken);
}

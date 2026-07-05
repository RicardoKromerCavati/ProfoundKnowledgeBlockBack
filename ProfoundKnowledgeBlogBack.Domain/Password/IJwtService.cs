using System.Security.Claims;

namespace ProfoundKnowledgeBlogBack.Domain.Password;

public interface IJwtService
{
    string CreateToken(string email, string username, Guid userIdentifier);
    ClaimsPrincipal ValidateToken(string authToken);
}

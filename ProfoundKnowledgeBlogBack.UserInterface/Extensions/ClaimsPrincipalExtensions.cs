
using System.Security.Claims;

namespace ProfoundKnowledgeBlogBack.UserInterface.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserIdentifier(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
            throw new UnauthorizedAccessException("User ID claim not found.");
    }
}

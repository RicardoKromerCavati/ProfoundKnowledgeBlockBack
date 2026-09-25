using ProfoundKnowledgeBlogBack.Application.Dtos;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using System.Security.Claims;

namespace ProfoundKnowledgeBlogBack.Application.Session;

public class SessionValidationUseCase(IJwtService jwtService) : ISessionValidationUseCase
{
    public async ValueTask<OperationResult<UserSessionDto>> ValidateSession(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return OperationResult<UserSessionDto>.Error("Invalid token");
        }

        token = token.Replace("Bearer ", string.Empty);

        string email;
        string username;

        try
        {
            var claimsPrincipal = jwtService.ValidateToken(token);
            email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value!;
            username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value!;
        }
        catch
        {
            return OperationResult<UserSessionDto>.Error("Invalid token");
        }

        return OperationResult<UserSessionDto>.Success(new UserSessionDto { Token = token, Email = email, Username = username });
    }
}
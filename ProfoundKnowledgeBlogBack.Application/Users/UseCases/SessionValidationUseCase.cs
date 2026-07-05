using Microsoft.Extensions.Options;
using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using System.Security.Claims;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public class SessionValidationUseCase(IJwtService jwtService) : ISessionValidationUseCase
{
    public async ValueTask<OperationResult<UserSessionResponse>> ValidateSession(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return OperationResult<UserSessionResponse>.Error("Invalid token");
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
            return OperationResult<UserSessionResponse>.Error("Invalid token");
        }

        return OperationResult<UserSessionResponse>.Success(new UserSessionResponse { Token = token, Email = email, Username = username });
    }
}
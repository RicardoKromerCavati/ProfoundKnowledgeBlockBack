using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProfoundKnowledgeBlogBack.Application.Users;
using ProfoundKnowledgeBlogBack.Domain.Password;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProfoundKnowledgeBlogBack.Application.Password;

public class JwtService(IOptions<AppSettings> options) : IJwtService
{
    public string CreateToken(string email, string username, Guid userIdentifier)
    {
        var appSettings = options.Value;

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.NameIdentifier, userIdentifier.ToString())
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims, 
            expires: DateTime.Now.AddMinutes(10), 
            signingCredentials: signingCredentials,
            issuer: appSettings.Issuer,
            audience: appSettings.Audience);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public ClaimsPrincipal ValidateToken(string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.Value.Key);
        var validationParameters = GetValidationParameters(key);
        return tokenHandler.ValidateToken(authToken, validationParameters, out _);
    }

    private static TokenValidationParameters GetValidationParameters(byte[] key)
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    }
}

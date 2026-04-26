using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProfoundKnowledgeBlogBack.Domain.Password;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProfoundKnowledgeBlogBack.Application.Password;

public class JwtService : IJwtService
{
    public string CreateToken(string keyValue, string email, string username)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Name, username)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signingCredentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public ClaimsPrincipal ValidateToken(string keyStr, string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(keyStr);
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

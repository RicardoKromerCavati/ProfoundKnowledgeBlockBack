using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Authentication.Login;

public class UserLoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
    
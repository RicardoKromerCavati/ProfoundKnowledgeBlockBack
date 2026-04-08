using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Authentication.Login;

public class UserLoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

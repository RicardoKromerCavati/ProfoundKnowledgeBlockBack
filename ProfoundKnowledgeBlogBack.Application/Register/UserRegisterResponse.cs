using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Register;

public class UserRegisterResponse
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}

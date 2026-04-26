using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Register;

public class UserRegisterRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}

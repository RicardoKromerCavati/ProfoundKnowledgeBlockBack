using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Authentication.Login;

public class UserLoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}

using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Register;

public class UserRegisterResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.UserInterface.Responses.Register;

public class UserRegisterResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
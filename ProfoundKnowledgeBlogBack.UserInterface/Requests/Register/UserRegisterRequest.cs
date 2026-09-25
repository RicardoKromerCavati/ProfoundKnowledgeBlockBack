using ProfoundKnowledgeBlogBack.Application.Register;
using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.UserInterface.Requests.Register;

public class UserRegisterRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    public static implicit operator UserRegisterCommand(UserRegisterRequest request) => new UserRegisterCommand
    {
        Email = request.Email,
        Password = request.Password,
        Username = request.Username
    };
}

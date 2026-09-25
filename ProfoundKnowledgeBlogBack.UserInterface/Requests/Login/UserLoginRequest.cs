using ProfoundKnowledgeBlogBack.Application.Login;
using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.UserInterface.Requests.Login;

public class UserLoginRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    public static implicit operator UserLoginCommand(UserLoginRequest request) => new UserLoginCommand
    {
        Password = request.Password,
        Email = request.Email
    };
}
    
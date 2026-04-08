using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Authentication.Login;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("login")]
    public async ValueTask<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest userLogin)
    {
        var response = new UserLoginResponse { Token = Guid.NewGuid().ToString() };
        return Ok(response);
    }
}
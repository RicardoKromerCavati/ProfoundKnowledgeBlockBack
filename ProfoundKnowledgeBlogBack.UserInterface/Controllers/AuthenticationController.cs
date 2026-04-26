using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Authentication.Login;
using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(ILoginUserUseCase loginUserUseCase) : ControllerBase
{
    [HttpPost]
    public async ValueTask<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        var result = await loginUserUseCase.LogUserIn(userLoginRequest);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value);
    }
}
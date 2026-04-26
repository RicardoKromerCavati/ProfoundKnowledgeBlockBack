using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Register;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController(IRegisterUserUseCase registerUserUseCase) : ControllerBase
{
    [HttpPost]
    public async ValueTask<ActionResult<UserRegisterResponse>> RegisterUser([FromBody] UserRegisterRequest userRegisterRequest)
    {
        var (isValid, message) = await registerUserUseCase.CreateUser(userRegisterRequest);

        if (!isValid)
        {
            return BadRequest(message);
        }

        return Ok(new UserRegisterResponse
        {
            Email = userRegisterRequest.Email,
            Username = userRegisterRequest.Email,
            Token = Guid.NewGuid().ToString()
        });
    }
}

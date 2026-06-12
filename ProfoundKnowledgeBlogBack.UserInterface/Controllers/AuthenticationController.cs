using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Authentication.Login;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;
using ProfoundKnowledgeBlogBack.UserInterface.Responses;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(ILoginUserUseCase loginUserUseCase) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IResult> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        var result = await loginUserUseCase.LogUserIn(userLoginRequest);

        if (!result.IsSuccessful)
        {
            return ProblemResponse.Create(StatusCodes.Status400BadRequest, "Invalid email or password", result.ErrorMessage);
        }

        return Results.Ok(result.Value);
    }
}
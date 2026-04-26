using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController(ISessionValidationUseCase sessionValidationUseCase) : ControllerBase
{
    [HttpGet]
    public async ValueTask<ActionResult<UserSessionResponse>> IsSessionValid()
    {
        var result = await sessionValidationUseCase.ValidateSession(Request.Headers.Authorization.ToString());

        if (result.IsSuccessful)
        {
            return Ok(result.Value);

        }

        return Unauthorized();
    }
}

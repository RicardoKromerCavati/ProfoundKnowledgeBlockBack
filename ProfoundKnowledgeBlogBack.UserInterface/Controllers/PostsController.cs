using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfoundKnowledgeBlogBack.Application.Posts;
using ProfoundKnowledgeBlogBack.Application.Posts.UseCases;
using ProfoundKnowledgeBlogBack.UserInterface.Extensions;

namespace ProfoundKnowledgeBlogBack.UserInterface.Controllers;

[ApiController, Authorize]
[Route("[controller]")]
public class PostsController(
    ICreatePostUseCase createUserUseCase,
    ILogger<PostsController> logger) : ControllerBase
{
    [HttpPost("create")]
    public async ValueTask<IResult> Create(CreatePostsRequest createPostsRequest)
    {
        try
        {
            var userIdentifier = User.GetUserIdentifier();

            var result = await createUserUseCase.CreatePost(new Guid(userIdentifier), createPostsRequest);

            if (!result.IsSuccessful)
            {
                return Results.BadRequest(result.ErrorMessage);
            }

            return Results.Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not save post");
            return Results.BadRequest("");
        }
    }
}
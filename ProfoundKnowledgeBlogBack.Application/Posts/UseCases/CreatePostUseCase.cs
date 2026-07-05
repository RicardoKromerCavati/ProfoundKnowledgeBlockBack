using Microsoft.Extensions.Logging;
using ProfoundKnowledgeBlogBack.Domain.Posts;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Posts.UseCases;

public class CreatePostUseCase(
    IPostRepository postRepository,
    IUserRepository userRepository,
    ILogger<CreatePostUseCase> logger) : ICreatePostUseCase
{
    public async ValueTask<OperationResult> CreatePost(Guid userIdentifier, CreatePostsRequest createPostRequest)
    {
        var count = await userRepository.SelectCountByUserId(userIdentifier);

        if (count <= 0)
        {
            logger.LogError("User {UserId} not found, post not saved", userIdentifier);
            return OperationResult.Error("Could not save new post");
        }

        if (string.IsNullOrWhiteSpace(createPostRequest.Title))
        {
            return OperationResult.Error("The post must have a title");
        }

        if (string.IsNullOrWhiteSpace(createPostRequest.Title))
        {
            return OperationResult.Error("The post must have content");
        }

        if (string.IsNullOrWhiteSpace(createPostRequest.Title))
        {
            return OperationResult.Error("The post must have an image");
        }

        var businessPost = new BusinessPost()
        {
            Content = createPostRequest.Content,
            ImageBase64 = createPostRequest.ImageBase64,
            Title = createPostRequest.Title,
            UserId = userIdentifier
        };

        await postRepository.Insert(businessPost);

        return OperationResult.Success();
    }
}

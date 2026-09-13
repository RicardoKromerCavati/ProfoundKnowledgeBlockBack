using Microsoft.Extensions.Logging;
using ProfoundKnowledgeBlogBack.Domain.Posts;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Posts.UseCases;

public class CreatePostUseCase(
    IPostRepository postRepository,
    IUserRepository userRepository,
    IHtmlCleaner htmlCleaner,
    IQuillDeltaCleaner quillDeltaCleaner,
    IImageProcessor imageProcessor,
    ILogger<CreatePostUseCase> logger) : ICreatePostUseCase
{
    public async ValueTask<OperationResult> CreatePost(Guid userIdentifier, CreatePostsRequest createPostRequest)
    {
        try
        {
            var count = await userRepository.SelectCountByUserId(userIdentifier);

            if (count <= 0)
            {
                logger.LogError("User {UserId} not found, post not saved", userIdentifier);
                return OperationResult.Error("Could not save new post");
            }

            var cleanTitle = htmlCleaner.Sanitize(createPostRequest.Title);

            var cleanContent = quillDeltaCleaner.Sanitize(createPostRequest.Content);

            var cleanImageInBytes = await imageProcessor.SanitizeBase64(createPostRequest.ImageBase64);

            var relativePath = await imageProcessor.Save("posts", cleanImageInBytes);

            var post =
                new Post(
                    userIdentifier,
                    cleanTitle,
                    cleanContent,
                    relativePath);

            await postRepository.Insert(post);

            return OperationResult.Success();
        }
        catch (Exception e)
        {
            return OperationResult.Error(e.Message);
        }
    }
}

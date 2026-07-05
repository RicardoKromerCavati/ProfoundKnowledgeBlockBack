using ProfoundKnowledgeBlogBack.Application.Authentication.Login;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Application.Posts.UseCases;

public interface ICreatePostUseCase
{
    ValueTask<OperationResult> CreatePost(Guid userIdentifier, CreatePostsRequest createPostRequest);
}
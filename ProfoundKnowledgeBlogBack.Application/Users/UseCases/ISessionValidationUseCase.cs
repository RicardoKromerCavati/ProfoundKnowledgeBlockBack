using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public interface ISessionValidationUseCase
{
    ValueTask<OperationResult<UserSessionResponse>> ValidateSession(string token);
}

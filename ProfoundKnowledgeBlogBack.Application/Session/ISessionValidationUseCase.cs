using ProfoundKnowledgeBlogBack.Application.Dtos;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Application.Session;

public interface ISessionValidationUseCase
{
    ValueTask<OperationResult<UserSessionDto>> ValidateSession(string token);
}

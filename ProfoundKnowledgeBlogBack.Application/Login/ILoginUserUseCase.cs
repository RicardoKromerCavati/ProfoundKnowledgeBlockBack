using ProfoundKnowledgeBlogBack.Application.Dtos;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Application.Login;

public interface ILoginUserUseCase
{
    ValueTask<OperationResult<UserLoginDto>> LogUserIn(UserLoginCommand userLoginRequest);
}
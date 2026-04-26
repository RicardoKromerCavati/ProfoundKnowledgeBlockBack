using ProfoundKnowledgeBlogBack.Application.Authentication.Login;
using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public interface ILoginUserUseCase
{
    ValueTask<OperationResult<UserLoginResponse>> LogUserIn(UserLoginRequest userLoginRequest);
}
using ProfoundKnowledgeBlogBack.Application.Register;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public interface IRegisterUserUseCase
{
    ValueTask<(bool, string)> CreateUser(UserRegisterRequest userRegisterRequest);
}

namespace ProfoundKnowledgeBlogBack.Application.Register;

public interface IRegisterUserUseCase
{
    ValueTask<(bool, string)> CreateUser(UserRegisterCommand userRegisterRequest);
}

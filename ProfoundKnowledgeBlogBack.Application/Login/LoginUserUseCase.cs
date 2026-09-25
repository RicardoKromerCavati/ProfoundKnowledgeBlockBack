using ProfoundKnowledgeBlogBack.Application.Dtos;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Login;

public class LoginUserUseCase(
    IUserRepository userRepository,
    IPasswordService passwordService,
    IJwtService jwtService) : ILoginUserUseCase
{
    public async ValueTask<OperationResult<UserLoginDto>> LogUserIn(UserLoginCommand userLoginRequest)
    {
        if (await userRepository.SelectCountByEmail(userLoginRequest.Email) <= 0)
        {
            return OperationResult<UserLoginDto>.Error("Invalid email or password");
        }

        var user = await userRepository.SelectDbUserByEmail(userLoginRequest.Email);

        if (user == null)
        {
            return OperationResult<UserLoginDto>.Error("Invalid email or password");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return OperationResult<UserLoginDto>.Error("Invalid email or password");
        }

        if (!passwordService.IsValid(userLoginRequest.Password, user.PasswordHash))
        {
            return OperationResult<UserLoginDto>.Error("Invalid email or password");
        }

        var token = jwtService.CreateToken(userLoginRequest.Email, user.Username, user.UserId);

        return OperationResult<UserLoginDto>.Success(new UserLoginDto { Email = user.Email, Token = token, Username = user.Username });
    }
}
using Microsoft.Extensions.Options;
using ProfoundKnowledgeBlogBack.Application.Authentication.Login;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public class LoginUserUseCase(
    IUserRepository userRepository,
    IPasswordService passwordService,
    IJwtService jwtService,
    IOptions<AppSettings> options) : ILoginUserUseCase
{
    public async ValueTask<OperationResult<UserLoginResponse>> LogUserIn(UserLoginRequest userLoginRequest)
    {
        if (await userRepository.SelectCountByEmail(userLoginRequest.Email) <= 0)
        {
            return OperationResult<UserLoginResponse>.Error("Invalid email or password");
        }

        var user = await userRepository.SelectDbUserByEmail(userLoginRequest.Email);

        if (user == null)
        {
            return OperationResult<UserLoginResponse>.Error("Invalid email or password");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return OperationResult<UserLoginResponse>.Error("Invalid email or password");
        }

        if (!passwordService.IsValid(userLoginRequest.Password, user.PasswordHash))
        {
            return OperationResult<UserLoginResponse>.Error("Invalid email or password");
        }

        var token = jwtService.CreateToken(options.Value.Key, userLoginRequest.Email, user.Username);

        return OperationResult<UserLoginResponse>.Success(new UserLoginResponse { Email = user.Email, Token = token, Username = user.Username });
    }
}
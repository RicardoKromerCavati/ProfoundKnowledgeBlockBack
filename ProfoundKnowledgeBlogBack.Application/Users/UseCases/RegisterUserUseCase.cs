using ProfoundKnowledgeBlogBack.Application.Register;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Users;
using System.Security.Cryptography;
using System.Text;

namespace ProfoundKnowledgeBlogBack.Application.Users.UseCases;

public class RegisterUserUseCase(IUserRepository userRepository, IPasswordService passwordService) : IRegisterUserUseCase
{
    public async ValueTask<(bool, string)> CreateUser(UserRegisterRequest userRegisterRequest)
    {
        try
        {
            var (isValid, message) = await ValidateRegisterUserRequest(userRegisterRequest);

            if (!isValid)
            {
                return (false, message);
            }

            var password = passwordService.Create(userRegisterRequest.Password);

            var user = new BusinessUser
            {
                Email = userRegisterRequest.Email,
                PasswordHash = password,
                Username = userRegisterRequest.Username,
            };

            await userRepository.InsertUser(user);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    private async ValueTask<(bool, string)> ValidateRegisterUserRequest(UserRegisterRequest userRegisterRequest)
    {
        if (string.IsNullOrEmpty(userRegisterRequest.Password) ||
            string.IsNullOrEmpty(userRegisterRequest.Email) ||
            string.IsNullOrEmpty(userRegisterRequest.Username))
        {
            return (false, "Please fill all fields");
        }

        if (await userRepository.SelectCountByEmail(userRegisterRequest.Email) > 0)
        {
            return (false, "Invalid e-mail");
        }

        if (await userRepository.SelectCountByUsername(userRegisterRequest.Username) > 0)
        {
            return (false, "Invalid username");
        }

        return (true, string.Empty);
    }
}

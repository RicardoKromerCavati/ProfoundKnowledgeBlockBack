using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Application.Register;

public class RegisterUserUseCase(IUserRepository userRepository, IPasswordService passwordService) : IRegisterUserUseCase
{
    public async ValueTask<(bool, string)> CreateUser(UserRegisterCommand userRegisterRequest)
    {
        try
        {
            var (isValid, message) = await ValidateRegisterUserRequest(userRegisterRequest);

            if (!isValid)
            {
                return (false, message);
            }

            var isStrongEnough = passwordService.IsStrong(userRegisterRequest.Password);

            if (!isStrongEnough)
            {
                return (false, string.Empty);
            }

            var password = passwordService.Create(userRegisterRequest.Password);

            var user = new User
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

    private async ValueTask<(bool, string)> ValidateRegisterUserRequest(UserRegisterCommand userRegisterRequest)
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

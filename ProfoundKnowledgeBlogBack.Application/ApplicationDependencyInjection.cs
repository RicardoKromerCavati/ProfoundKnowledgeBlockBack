using Microsoft.Extensions.DependencyInjection;
using ProfoundKnowledgeBlogBack.Application.Login;
using ProfoundKnowledgeBlogBack.Application.Password;
using ProfoundKnowledgeBlogBack.Application.Posts.UseCases;
using ProfoundKnowledgeBlogBack.Application.Register;
using ProfoundKnowledgeBlogBack.Application.Session;
using ProfoundKnowledgeBlogBack.Domain.Password;

namespace ProfoundKnowledgeBlogBack.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
        serviceCollection.AddTransient<ILoginUserUseCase, LoginUserUseCase>();
        serviceCollection.AddTransient<ISessionValidationUseCase, SessionValidationUseCase>();
        serviceCollection.AddTransient<ICreatePostUseCase, CreatePostUseCase>();
        serviceCollection.AddTransient<IPasswordService, PasswordService>();
        serviceCollection.AddTransient<IJwtService, JwtService>();
    }
}
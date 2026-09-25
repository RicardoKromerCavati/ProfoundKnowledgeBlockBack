using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfoundKnowledgeBlogBack.Application.Interfaces;
using ProfoundKnowledgeBlogBack.Application.Interfaces.Services;
using ProfoundKnowledgeBlogBack.Domain.Posts;
using ProfoundKnowledgeBlogBack.Domain.Users;
using ProfoundKnowledgeBlogBack.Infrastructure.ExternalServices.Images;
using ProfoundKnowledgeBlogBack.Infrastructure.Posts;
using ProfoundKnowledgeBlogBack.Infrastructure.Queries;
using ProfoundKnowledgeBlogBack.Infrastructure.Text;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlDatabase");
        serviceCollection
            .AddDbContext<ProfoundKnowledgeContext>(
                options => 
                    options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString)));

        serviceCollection.AddTransient<IUserRepository, UserRepository>();
        serviceCollection.AddTransient<IPostRepository, PostRepository>();
        serviceCollection.AddTransient<IPostQueries, PostQueries>();
        serviceCollection.AddSingleton<IHtmlCleaner, HtmlCleaner>();
        serviceCollection.AddSingleton<IQuillDeltaCleaner, QuillDeltaCleaner>();
        serviceCollection.AddSingleton<IImageSanitizer, ImageProcessor>();
        serviceCollection.AddSingleton<IImageStoreService, ImageStoreService>();
    }
}
using ProfoundKnowledgeBlogBack.Application;
using ProfoundKnowledgeBlogBack.Infrastructure;
using ProfoundKnowledgeBlogBack.UserInterface;

Startup.ConfigureImageProcessor();

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

app.ConfigureStartup();

app.Run();
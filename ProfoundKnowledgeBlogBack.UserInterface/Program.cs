using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Application.Password;
using ProfoundKnowledgeBlogBack.Application.Users;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Users;
using ProfoundKnowledgeBlogBack.Infrastructure;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularInDevelopment", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProfoundKnowledgeContext>(options => options.UseInMemoryDatabase("ProfoundKnowledge"));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddTransient<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<ISessionValidationUseCase, SessionValidationUseCase>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

app.UseCors("AllowAngularInDevelopment");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

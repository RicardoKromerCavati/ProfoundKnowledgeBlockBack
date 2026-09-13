using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NetVips;
using ProfoundKnowledgeBlogBack.Application.Interfaces;
using ProfoundKnowledgeBlogBack.Application.Password;
using ProfoundKnowledgeBlogBack.Application.Posts.UseCases;
using ProfoundKnowledgeBlogBack.Application.Users;
using ProfoundKnowledgeBlogBack.Application.Users.UseCases;
using ProfoundKnowledgeBlogBack.Domain.Password;
using ProfoundKnowledgeBlogBack.Domain.Posts;
using ProfoundKnowledgeBlogBack.Domain.Shared;
using ProfoundKnowledgeBlogBack.Domain.Users;
using ProfoundKnowledgeBlogBack.Infrastructure;
using ProfoundKnowledgeBlogBack.Infrastructure.Posts;
using ProfoundKnowledgeBlogBack.Infrastructure.Queries;
using ProfoundKnowledgeBlogBack.Infrastructure.Shared;
using ProfoundKnowledgeBlogBack.Infrastructure.Users;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json;

Environment.SetEnvironmentVariable("VIPS_BLOCK_UNTRUSTED", "1");

Operation.Block("VipsForeignLoad", true);

Operation.Block("VipsForeignLoadJpeg", false);
Operation.Block("VipsForeignLoadPng", false);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

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

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<ProfoundKnowledgeContext>(options => options.UseInMemoryDatabase("ProfoundKnowledge"));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddTransient<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<ISessionValidationUseCase, SessionValidationUseCase>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<ICreatePostUseCase, CreatePostUseCase>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IHtmlCleaner, HtmlCleaner>();
builder.Services.AddSingleton<IQuillDeltaCleaner, QuillDeltaCleaner>();
builder.Services.AddSingleton<IImageProcessor, ImageProcessor>();
builder.Services.AddTransient<IPostQueries, PostQueries>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
        ctx.ProblemDetails.Extensions.TryAdd("requestId", ctx.HttpContext.TraceIdentifier);

        var activity = ctx.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        ctx.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          ValidateIssuer = true,
          ValidateAudience = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value!)),
          ValidAudience = builder.Configuration.GetSection("AppSettings:Audience").Value!,
          ValidIssuer = builder.Configuration.GetSection("AppSettings:Issuer").Value!
      };
  });

var app = builder.Build();

using var s = app.Services.CreateScope();

var provider = s.ServiceProvider;

var c = provider.GetRequiredService<ProfoundKnowledgeContext>();

var user = new User()
{
    Email = "ricardo@gmail.com",
    UserId = Guid.NewGuid(),
    Username = "ricardo",
    PasswordHash = "a"
};

c.Users.Add(user);

c.SaveChanges();

app.UseCors("AllowAngularInDevelopment");

if (app.Environment.IsProduction() is false)
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
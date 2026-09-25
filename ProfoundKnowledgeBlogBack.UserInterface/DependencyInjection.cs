using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ProfoundKnowledgeBlogBack.Application.Users;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json;

namespace ProfoundKnowledgeBlogBack.UserInterface;

public static class DependencyInjection
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
            if (builder.Environment.IsProduction() is false)
            {

                options.AddPolicy("AllowAngularInDevelopment", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            }
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

        builder.Services.Configure<TokenInformation>(builder.Configuration.GetSection("TokenInformation"));

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
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenInformation:Key").Value!)),
                  ValidAudience = builder.Configuration.GetSection("TokenInformation:Audience").Value!,
                  ValidIssuer = builder.Configuration.GetSection("TokenInformation:Issuer").Value!
              };
          });
    }
}

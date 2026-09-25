using NetVips;

namespace ProfoundKnowledgeBlogBack.UserInterface;

public static class Startup
{
    public static void ConfigureStartup(this WebApplication webApplication)
    {
        webApplication.UseCors("AllowAngularInDevelopment");

        if (webApplication.Environment.IsProduction() is false)
        {
            webApplication.MapOpenApi();
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        webApplication.UseHttpsRedirection();

        webApplication.UseAuthentication();

        webApplication.UseAuthorization();

        webApplication.MapControllers();
    }

    public static void ConfigureImageProcessor()
    {
        Environment.SetEnvironmentVariable("VIPS_BLOCK_UNTRUSTED", "1");

        Operation.Block("VipsForeignLoad", true);

        Operation.Block("VipsForeignLoadJpeg", false);
        Operation.Block("VipsForeignLoadPng", false);
    }
}

using ProfoundKnowledgeBlogBack.Application.Interfaces.Services;

namespace ProfoundKnowledgeBlogBack.Infrastructure.ExternalServices.Images;

public class ImageStoreService : IImageStoreService
{
    private const string WwwRoot = "wwwroot";

    public async ValueTask<string> Save(string directory, byte[] cleanImageInBytes)
    {
        var fileName = $"{Guid.NewGuid()}.jpg";

        var storagePath = Path.Combine(Directory.GetCurrentDirectory(), WwwRoot, directory);

        var fullPath = Path.Combine(storagePath, fileName);

        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }

        await File.WriteAllBytesAsync(fullPath, cleanImageInBytes);

        var relativePath = $"/{directory}/{fileName}";

        return relativePath;
    }
}

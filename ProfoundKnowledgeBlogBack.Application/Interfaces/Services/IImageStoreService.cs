namespace ProfoundKnowledgeBlogBack.Application.Interfaces.Services;

public interface IImageStoreService
{
    ValueTask<string> Save(string directory, byte[] cleanImageInBytes);
}

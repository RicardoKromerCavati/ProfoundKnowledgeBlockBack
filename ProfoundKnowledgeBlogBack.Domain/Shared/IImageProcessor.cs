namespace ProfoundKnowledgeBlogBack.Domain.Shared;

public interface IImageProcessor
{
    ValueTask<byte[]> SanitizeBase64(string base64Image);
    ValueTask<string> Save(string directory, byte[] cleanImageInBytes);
}
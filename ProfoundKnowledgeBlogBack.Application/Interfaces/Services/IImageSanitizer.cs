namespace ProfoundKnowledgeBlogBack.Application.Interfaces.Services;

public interface IImageSanitizer
{
    ValueTask<byte[]> SanitizeBase64(string base64Image);
}
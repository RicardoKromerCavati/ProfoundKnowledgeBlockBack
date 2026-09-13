using System.Text;
using NetVips;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Shared;

public class ImageProcessor : IImageProcessor
{
    private const string WwwRoot = "wwwroot";
    private const int MaxFileSize = 1 * 1024 * 1024; // 1 MB
    private const int MaxWidth = 4096;
    private const int MaxHeight = 4096;
    const long maxPixels = 16_000_000;

    public async ValueTask<byte[]> SanitizeBase64(string base64)
    {
        const int MaxFileSize = 5 * 1024 * 1024;

        byte[] bytes;

        try
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("Image cannot be empty.");

            var commaIndex = base64.IndexOf(',');

            if (base64.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                if (commaIndex < 0)
                    throw new ArgumentException("Invalid image Data URI.");

                base64 = base64[(commaIndex + 1)..];
            }

            bytes = Convert.FromBase64String(base64);

            Console.WriteLine(Convert.ToHexString(bytes.Take(16).ToArray()));
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid Base64.");
        }

        if (bytes.Length == 0 || bytes.Length > MaxFileSize)
            throw new ArgumentException("Invalid image size.");

        Image? image = null;

        try
        {
            image = Image.NewFromBuffer(
            bytes,
            access: Enums.Access.Sequential,
            failOn: Enums.FailOn.Error);
        }
        catch (VipsException)
        {
            var imageFormat = DetectFormat(bytes);
            throw new ArgumentException(
                $"The uploaded image is {imageFormat}. Only original JPEG and PNG images are supported.");
        }

        if (image.Width <= 0 || image.Height <= 0)
            throw new ArgumentException("Invalid image dimensions.");

        //if (image.Width > MaxWidth ||
        //    image.Height > MaxHeight)
        //    throw new ArgumentException("Image dimensions are too large.");

        //if ((long)image.Width * image.Height > MaxPixels)
        //    throw new ArgumentException("Image contains too many pixels.");

        var loader = image.Get("vips-loader")?.ToString();

        if (loader is not ("jpegload_buffer" or "pngload_buffer"))
        {
            var detectedFormat = loader switch
            {
                "heifload_buffer" => "HEIF/HEIC/AVIF",
                "webpload_buffer" => "WebP",
                _ => "unknown"
            };

            throw new ArgumentException(
                $"The uploaded image is {detectedFormat}. Only JPEG and PNG images are supported.");
        }

        using var sanitizedImage = image.Mutate(mutable =>
        {
            foreach (var field in image.GetFields())
            {
                mutable.Remove(field);
            }
        });

        return sanitizedImage.WriteToBuffer(
            ".jpg[Q=90,strip]");
    }

    private static string RemoveDataUriPrefix(string dirtyBase64Image)
    {
        if (dirtyBase64Image.Contains(','))
        {
            var parts = dirtyBase64Image.Split(',');
            dirtyBase64Image = parts[1];
        }

        return dirtyBase64Image;
    }

    private static bool IsAllowedFormat(string? loader)
    {
        if (string.IsNullOrWhiteSpace(loader))
            return false;

        System.Console.WriteLine("loader", loader);

        return loader.Equals(
                   "jpegload",
                   StringComparison.OrdinalIgnoreCase)
               ||
               loader.Equals(
                   "pngload",
                   StringComparison.OrdinalIgnoreCase);
    }

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

    private static string DetectFormat(byte[] bytes)
    {
        if (bytes.Length >= 3 &&
            bytes[0] == 0xFF &&
            bytes[1] == 0xD8 &&
            bytes[2] == 0xFF)
        {
            return "JPEG";
        }

        if (bytes.Length >= 8 &&
            bytes[0] == 0x89 &&
            bytes[1] == 0x50 &&
            bytes[2] == 0x4E &&
            bytes[3] == 0x47 &&
            bytes[4] == 0x0D &&
            bytes[5] == 0x0A &&
            bytes[6] == 0x1A &&
            bytes[7] == 0x0A)
        {
            return "PNG";
        }

        if (bytes.Length >= 12 &&
            bytes[4] == (byte)'f' &&
            bytes[5] == (byte)'t' &&
            bytes[6] == (byte)'y' &&
            bytes[7] == (byte)'p')
        {
            var brand = Encoding.ASCII.GetString(bytes, 8, 4);

            return brand switch
            {
                "avif" => "AVIF",
                "avis" => "AVIF",
                "heic" => "HEIC",
                "heix" => "HEIC",
                "hevc" => "HEVC",
                "mif1" => "HEIF",
                _ => "unknown"
            };
        }

        return "unknown";
    }
}
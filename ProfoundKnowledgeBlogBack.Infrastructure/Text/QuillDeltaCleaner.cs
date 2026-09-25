using ProfoundKnowledgeBlogBack.Application.Interfaces.Services;
using System.Text.Json.Nodes;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Text;

public class QuillDeltaCleaner : IQuillDeltaCleaner
{
    private static readonly HashSet<string> AllowedAttributes = new(StringComparer.OrdinalIgnoreCase)
{
    "bold", "italic", "underline", "strike",
    "blockquote", "code-block", "list", "header",
    "color", "background", "link"
};

    public string Sanitize(JsonNode deltaNode)
    {
        if (deltaNode is not JsonObject rootObj ||
            !rootObj.TryGetPropertyValue("ops", out var opsNode) ||
            opsNode is not JsonArray opsArray)
        {
            return deltaNode.ToString();
        }

        foreach (var op in opsArray)
        {
            if (op is not JsonObject opObj) continue;

            if (opObj.TryGetPropertyValue("insert", out var insertNode))
            {
                SanitizeInsert(opObj, insertNode);
            }

            if (opObj.TryGetPropertyValue("attributes", out var attrNode) && attrNode is JsonObject attrObj)
            {
                SanitizeAttributes(attrObj);
            }
        }

        return deltaNode.ToString();
    }

    private static void SanitizeInsert(JsonObject opObj, JsonNode insertNode)
    {
        if (insertNode is JsonValue && insertNode.GetValueKind() == System.Text.Json.JsonValueKind.String)
        {
            return;
        }

        if (insertNode is JsonObject insertObj)
        {
            var keysToRemove = new List<string>();

            foreach (var (key, value) in insertObj)
            {
                if (key.Equals("image", StringComparison.OrdinalIgnoreCase))
                {
                    string? imageUrl = value?.ToString();
                    if (!IsValidImageUrl(imageUrl))
                    {
                        keysToRemove.Add(key);
                    }
                }
                else
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                insertObj.Remove(key);
            }

            if (insertObj.Count == 0)
            {
                opObj.Remove("insert");
            }
        }
        else
        {
            opObj.Remove("insert");
        }
    }

    private static void SanitizeAttributes(JsonObject attributes)
    {
        var keysToRemove = new List<string>();

        foreach (var (key, value) in attributes)
        {
            if (!AllowedAttributes.Contains(key))
            {
                keysToRemove.Add(key);
                continue;
            }

            if (key.Equals("link", StringComparison.OrdinalIgnoreCase))
            {
                string? url = value?.ToString();
                if (!IsValidHttpOrMailtoUrl(url))
                {
                    keysToRemove.Add(key);
                }
            }
        }

        foreach (var key in keysToRemove)
        {
            attributes.Remove(key);
        }
    }

    private static bool IsValidHttpOrMailtoUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;
        string cleanUrl = url.Trim().ToLowerInvariant();

        if (cleanUrl.StartsWith("javascript:") || cleanUrl.StartsWith("vbscript:")) return false;

        if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var parsedUri))
        {
            if (!parsedUri.IsAbsoluteUri) return true;

            return parsedUri.Scheme == Uri.UriSchemeHttp ||
                   parsedUri.Scheme == Uri.UriSchemeHttps ||
                   parsedUri.Scheme == Uri.UriSchemeMailto;
        }

        return false;
    }

    private static bool IsValidImageUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;
        string cleanUrl = url.Trim().ToLowerInvariant();

        if (cleanUrl.StartsWith("data:image/png") ||
            cleanUrl.StartsWith("data:image/jpeg") ||
            cleanUrl.StartsWith("data:image/jpg") ||
            cleanUrl.StartsWith("data:image/gif") ||
            cleanUrl.StartsWith("data:image/webp"))
        {
            return true;
        }

        return IsValidHttpOrMailtoUrl(url);
    }
}
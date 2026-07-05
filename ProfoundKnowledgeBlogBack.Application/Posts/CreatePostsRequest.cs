using System.Text.Json.Serialization;

namespace ProfoundKnowledgeBlogBack.Application.Posts;

public class CreatePostsRequest
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("imageBase64")]
    public string ImageBase64 { get; set; } = string.Empty;
}
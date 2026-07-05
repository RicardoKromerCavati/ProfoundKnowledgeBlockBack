using ProfoundKnowledgeBlogBack.Domain.Posts;

namespace ProfoundKnowledgeBlogBack.Application.Posts;

public class BusinessPost : IPost
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageBase64 { get; set; } = string.Empty;
}
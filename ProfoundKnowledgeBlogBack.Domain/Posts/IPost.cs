namespace ProfoundKnowledgeBlogBack.Domain.Posts;

public interface IPost
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageBase64 { get; set; }
}

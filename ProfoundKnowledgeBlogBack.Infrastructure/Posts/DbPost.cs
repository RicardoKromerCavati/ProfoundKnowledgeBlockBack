using ProfoundKnowledgeBlogBack.Domain.Posts;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Posts;

public class DbPost : IPost
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageBase64 { get; set; }

    public DbPost(Guid posterId, Guid userId, string title, string content, string imageBase64)
    {
        PostId = posterId;
        UserId = userId;
        Title = title;
        Content = content;
        ImageBase64 = imageBase64;
    }

    public DbPost()
    {
        
    }

    public static DbPost Create(IPost post) => new(Guid.NewGuid(), post.UserId, post.Title, post.Content, post.ImageBase64);
}
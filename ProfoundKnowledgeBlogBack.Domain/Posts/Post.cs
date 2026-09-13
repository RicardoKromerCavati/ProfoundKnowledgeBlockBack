using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Domain.Posts;

public class Post
{
    public Post(Guid userId, string title, string content, string imageRelativePath)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException("The post must have a title");
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new InvalidOperationException("The post must have content");
        }

        if (string.IsNullOrWhiteSpace(imageRelativePath))
        {
            throw new InvalidOperationException("The post must have an image");
        }

        if (userId == Guid.Empty)
        {
            throw new InvalidOperationException("User identifier must be valid");
        }

        PostId = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Content = content;
        ImageRelativePath = imageRelativePath;
    }

    public Guid PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageRelativePath { get; set; } = string.Empty;

    // User relation
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
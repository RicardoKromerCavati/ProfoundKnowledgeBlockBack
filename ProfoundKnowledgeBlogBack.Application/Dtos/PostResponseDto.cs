namespace ProfoundKnowledgeBlogBack.Application.Dtos;

public class PostResponseDto
{
    public PostResponseDto(
    Guid postId,
    string content,
    string title,
    string relativeImagePath,
    string authorName)
    {
        if (postId == Guid.Empty)
        {
            throw new ArgumentException("Post ID cannot be empty.", nameof(postId));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content cannot be null or empty.", nameof(content));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(relativeImagePath))
        {
            throw new ArgumentException("Relative image path cannot be null or empty.", nameof(relativeImagePath));
        }

        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new ArgumentException("Author name cannot be null or empty.", nameof(authorName));
        }

        PostId = postId;
        Content = content;
        Title = title;
        RelativeImagePath = relativeImagePath;
        AuthorName = authorName;
    }

    public Guid PostId { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public string RelativeImagePath { get; set; }
    public string AuthorName { get; set; }
}
namespace ProfoundKnowledgeBlogBack.Domain.Posts;

public interface IPostRepository
{
    ValueTask Insert(Post post);
}

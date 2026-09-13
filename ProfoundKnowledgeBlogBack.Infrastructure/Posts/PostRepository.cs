using ProfoundKnowledgeBlogBack.Domain.Posts;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Posts;

public class PostRepository(ProfoundKnowledgeContext profoundKnowledgeContext) : IPostRepository
{
    public async ValueTask Insert(Post post)
    {
        profoundKnowledgeContext.Posts.Add(post);
        await profoundKnowledgeContext.SaveChangesAsync();
    }
}

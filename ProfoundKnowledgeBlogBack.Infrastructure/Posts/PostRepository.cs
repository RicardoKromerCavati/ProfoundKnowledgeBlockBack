using ProfoundKnowledgeBlogBack.Domain.Posts;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Posts;

public class PostRepository(ProfoundKnowledgeContext profoundKnowledgeContext) : IPostRepository
{
    public async ValueTask Insert(IPost post)
    {
        var dbPost = DbPost.Create(post);
        profoundKnowledgeContext.Posts.Add(dbPost);
        await profoundKnowledgeContext.SaveChangesAsync();
    }
}

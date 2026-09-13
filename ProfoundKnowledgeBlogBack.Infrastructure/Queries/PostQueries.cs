using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Application.Dtos;
using ProfoundKnowledgeBlogBack.Application.Interfaces;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Queries;

public class PostQueries(ProfoundKnowledgeContext profoundKnowledgeContext) : IPostQueries
{
    public async Task<List<PostResponseDto>> GetAllWithAuthorAsync()
    {
        return await 
            profoundKnowledgeContext
            .Posts
            .AsNoTracking()
            .Select(p => new PostResponseDto(p.PostId, p.Content, p.Title, p.ImageRelativePath, p.User.Username))
            .ToListAsync();
    }
}
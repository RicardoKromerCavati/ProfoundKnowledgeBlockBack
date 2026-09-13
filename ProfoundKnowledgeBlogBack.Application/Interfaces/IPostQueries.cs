using ProfoundKnowledgeBlogBack.Application.Dtos;

namespace ProfoundKnowledgeBlogBack.Application.Interfaces;

public interface IPostQueries
{
    Task<List<PostResponseDto>> GetAllWithAuthorAsync();
}
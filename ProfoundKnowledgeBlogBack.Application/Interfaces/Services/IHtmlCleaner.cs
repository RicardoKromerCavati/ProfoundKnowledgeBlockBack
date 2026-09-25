namespace ProfoundKnowledgeBlogBack.Application.Interfaces.Services;

public interface IHtmlCleaner
{
    string Sanitize(string dirtyHtml);
}
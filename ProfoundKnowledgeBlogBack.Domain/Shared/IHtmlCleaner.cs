namespace ProfoundKnowledgeBlogBack.Domain.Shared;

public interface IHtmlCleaner
{
    string Sanitize(string dirtyHtml);
}
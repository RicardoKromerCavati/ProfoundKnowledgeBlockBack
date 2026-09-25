using System.Text.Json.Nodes;

namespace ProfoundKnowledgeBlogBack.Application.Interfaces.Services;

public interface IQuillDeltaCleaner
{
    string Sanitize(JsonNode deltaNode);
}

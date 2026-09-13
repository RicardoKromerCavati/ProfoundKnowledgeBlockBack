using System.Text.Json.Nodes;

namespace ProfoundKnowledgeBlogBack.Domain.Shared;

public interface IQuillDeltaCleaner
{
    string Sanitize(JsonNode deltaNode);
}

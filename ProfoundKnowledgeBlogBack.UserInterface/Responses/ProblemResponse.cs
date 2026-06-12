using Microsoft.AspNetCore.WebUtilities;

namespace ProfoundKnowledgeBlogBack.UserInterface.Responses;

class ProblemResponse
{
    public static IResult Create(int statusCode, string title, string detail) =>
     Results.Problem(type: ReasonPhrases.GetReasonPhrase(statusCode),
                        title: title,
                        detail: detail,
                        statusCode: statusCode);
}
using Ganss.Xss;
using ProfoundKnowledgeBlogBack.Application.Interfaces.Services;
using ProfoundKnowledgeBlogBack.Domain.Shared;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Text;

public class HtmlCleaner : IHtmlCleaner
{
    private readonly HtmlSanitizer _htmlSanitizer;

    public HtmlCleaner()
    {
        _htmlSanitizer = new HtmlSanitizer();

        _htmlSanitizer.AllowedTags.Clear();
        _htmlSanitizer.AllowedAttributes.Clear();
        _htmlSanitizer.AllowedCssProperties.Clear();

        _htmlSanitizer.AllowedTags.UnionWith([
            "p",
            "br",
            "strong",
            "em",
            "u",
            "s",
            "blockquote",
            "pre",
            "code",
            "ol",
            "ul",
            "li",
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6",
            "a"
        ]);

        _htmlSanitizer.AllowedAttributes.UnionWith(["href"]);

        _htmlSanitizer.AllowedAttributes.Add("style");

        _htmlSanitizer.AllowedCssProperties.UnionWith([
            "color",
            "background-color"
        ]);

        _htmlSanitizer.AllowedSchemes.Clear();
        _htmlSanitizer.AllowedSchemes.Add("https");
        _htmlSanitizer.AllowedSchemes.Add("http");
    }

    public string Sanitize(string dirtyHtml) => _htmlSanitizer.Sanitize(dirtyHtml);
}
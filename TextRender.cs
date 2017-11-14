using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class TextRender
    {
        private List<MarkupRule> CurrentMarkupRules { get; }

        public TextRender(List<MarkupRule> rules)
        {
            CurrentMarkupRules = rules;
        }
        
        public string RenderLine(string s, IEnumerable<ParsedSubline> parsed)
        {
            var indexAndTagPairs = GetHtmlTagsOrderedByIndex(parsed);
            var offset = 0;
            var result = new StringBuilder(s);
            foreach (var pair in indexAndTagPairs)
            {
                var tag = GetHtmlTag(pair.Item2);
                result.Insert(pair.Item1 + offset, tag);
                offset += tag.Length;
            }
            return result.ToString();

        }

        private string GetHtmlTag(ToHtmlTag obj)
        {
            var markupRule = CurrentMarkupRules
                .First(e => e.HtmlTag == obj.TagName);
            return obj.IsClosingTag ? $"<\\{obj.TagName}>" : $"<{obj.TagName}>";
        }

        private static IEnumerable<(int, ToHtmlTag)> GetHtmlTagsOrderedByIndex(IEnumerable<ParsedSubline> parsed)
        {
            var insertedTags = new List<(int, ToHtmlTag)>();
            foreach (var subline in parsed)
            {
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new ToHtmlTag(subline.MarkupRule.HtmlTag, false)));
                insertedTags.Add(
                    (subline.RightBorderOfSubline, new ToHtmlTag(subline.MarkupRule.HtmlTag, true)));
            }
            return insertedTags
                .OrderBy(e => e.Item1)
                .ToList();
        }
    }
}
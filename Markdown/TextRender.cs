using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown
{
    public class TextRender
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }

        public TextRender(List<IMarkupRule> rules)
        {
            CurrentMarkupRules = rules;
        }
        
        public string RenderLine(string line, IEnumerable<ParsedSubline> parsed)
        {
            var indexAndTagPairs = GetHtmlTagsOrderedByIndex(parsed);
            var offset = 0;
            var result = new StringBuilder(line);
            
            foreach (var valueTuple in indexAndTagPairs)
            {
                var tag = GetHtmlTag(valueTuple.Item2);
                if (valueTuple.Item1 == line.Length)
                    result.Append(tag);
                else
                {
                    result.Remove(valueTuple.Item1 + offset, valueTuple.Item2.LenghtOfReplacedTag);
                    result.Insert(valueTuple.Item1 + offset, tag);
                    offset += tag.Length - valueTuple.Item2.LenghtOfReplacedTag;   
                }
            }
            return result.ToString();
        }

        private string GetHtmlTag(ToHtmlTag obj)
        {
            var markupRule = CurrentMarkupRules
                .First(e => e.HtmlTag == obj.TagName);
            return obj.IsClosingTag ? $@"</{obj.TagName}>" : $"<{obj.TagName}>";
        }

        private static IEnumerable<(int, ToHtmlTag)> GetHtmlTagsOrderedByIndex(IEnumerable<ParsedSubline> parsed)
        {
            var insertedTags = new List<(int, ToHtmlTag)>();
            foreach (var subline in parsed)
            {
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new ToHtmlTag(subline.MarkupRule.HtmlTag, false, subline.MarkupRule.MarkupTag.Length)));
                insertedTags.Add(
                    (subline.RightBorderOfSubline, new ToHtmlTag(subline.MarkupRule.HtmlTag, true, subline.MarkupRule.MarkupTag.Length)));
            }
            return insertedTags
                .OrderBy(e => e.Item1)
                .ToList();
        }
    }
}
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
            var indexAndTagValueTuples = GetHtmlTagsOrderedByIndex(parsed);
            var offsetAfterReplacingTags = 0;
            var result = new StringBuilder(line);
            
            foreach (var valueTuple in indexAndTagValueTuples)
            {
                var tag = GetHtmlTagFromMarkup(valueTuple.Item2);
                if (valueTuple.Item1 == line.Length)
                    result.Append(tag);
                else
                {
                    var startIndex = valueTuple.Item1 + offsetAfterReplacingTags;
                    var markupTagLenght = valueTuple.Item2.LenghtOfReplacedMarkupTag;
                    
                    result.Remove(startIndex, markupTagLenght);
                    result.Insert(startIndex, tag);
                    offsetAfterReplacingTags += tag.Length - markupTagLenght;   
                }
            }
            return result.ToString();
        }

        private string GetHtmlTagFromMarkup(FromMarkupTagToHtml obj)
        {
            var markupRule = CurrentMarkupRules
                .First(e => e.HtmlTag == obj.TagName);
            return obj.IsClosingHtmlTag ? $@"</{obj.TagName}>" : $"<{obj.TagName}>";
        }

        private static IEnumerable<(int, FromMarkupTagToHtml)> GetHtmlTagsOrderedByIndex(IEnumerable<ParsedSubline> parsed)
        {
            var insertedTags = new List<(int, FromMarkupTagToHtml)>();
            
            foreach (var subline in parsed)
            {
                var htmlTag = subline.MarkupRule.HtmlTag;
                var lenght = subline.MarkupRule.MarkupTag.Length;
                insertedTags.Add(
                    (subline.LeftBorderOfSubline, new FromMarkupTagToHtml(htmlTag, false, lenght)));
                insertedTags.Add(
                    (subline.RightBorderOfSubline, new FromMarkupTagToHtml(htmlTag, true, lenght)));
            }
            
            return insertedTags
                .OrderBy(e => e.Item1)
                .ToList();
        }
    }
}
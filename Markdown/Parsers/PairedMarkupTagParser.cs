using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown.Parsers
{
    public class PairedMarkupTagParser : IMarkupTagsParser
    {
        public IEnumerable<ParsedSubline> ParseLine(string line, string markupTag)
        {
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            for (var i = 0; i < line.Length - markupTag.Length; i++)
            {
                if (line.Substring(i, markupTag.Length) != markupTag) continue;

                if (Utils.CanBeOpenningTag(line, i))
                {
                    var subline = new ParsedSubline
                    {
                        LeftBorderOfSubline = i,
                    };
                    stack.Push(subline);
                }
                else if (Utils.CanBeClosingTag(line, i, markupTag.Length))
                {
                    var element = stack.Count > 0 ? stack.Pop() : null;
                    if (element == null) continue;
                    element.RightBorderOfSubline = i;
                    result.Add(element);
                }
                i += markupTag.Length;
            }
            return result;
        }
    }
}
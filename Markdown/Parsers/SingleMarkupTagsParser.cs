using System.Collections.Generic;
using Markdown.MarkupRules;

namespace Markdown.Parsers
{
    public class SingleMarkupTagsParser : IMarkupTagsParser
    {
        public IEnumerable<ParsedSubline> ParseLine(string line, string markupTag)
        {
            var result = new List<ParsedSubline>();
            
            if (line.StartsWith($"{markupTag} "))
                result.Add(
                    new ParsedSubline()
                    {
                        LeftBorderOfSubline = 0, 
                        RightBorderOfSubline = markupTag.Length - 1
                    });
            return result;
        }

    }
}
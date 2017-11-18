using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Header : MarkupRule
    {
        public override string MarkupTag { get; } = "#";
        public override string HtmlTag { get; } = "h1";
        
        public override IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            
            if (line.StartsWith($"{MarkupTag} "))
                result.Add(
                    new ParsedSubline()
                    {
                        LeftBorderOfSubline = 0, 
                        RightBorderOfSubline = MarkupTag.Length - 1, 
                        MarkupRule = this
                    });
            return result;
        }
        
    }
}

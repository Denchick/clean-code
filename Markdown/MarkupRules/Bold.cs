using System.Collections.Generic;

namespace Markdown.MarkupRules
{
    internal class Bold : MarkupRule
    {
        public override string MarkupTag { get; } = "__";
        public override string HtmlTag { get; } = "strong";

        public override IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            for (var i = 0; i < line.Length - MarkupTag.Length; i++)
            {
                if (line.Substring(i, MarkupTag.Length) != MarkupTag) continue;

                if (Utils.CanBeOpenningTag(line, i))
                {
                    var subline = new ParsedSubline
                    {
                        LeftBorderOfSubline = i,
                        // зачем тут this? внешнему миру пофиг на то, как правило парсится, ему важен только HtmlTag
                        // Да, можно было бы сделать так, чтобы его не указывать, но попозже.
                        MarkupRule = this
                    };
                    stack.Push(subline);
                }
                else if (Utils.CanBeClosingTag(line, i, MarkupTag.Length))
                {
                    var element = stack.Count > 0 ? stack.Pop() : null;
                    if (element == null) continue;
                    element.RightBorderOfSubline = i;
                    result.Add(element);
                }
                i += MarkupTag.Length;
            }
            return result;
        }
    }
}

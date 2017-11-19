using System.Collections.Generic;
using System.Linq;
using Markdown.MarkupRules;

namespace Markdown.Parsers
{
    public class PairedMarkupTagParser : IMarkupTagsParser
    {
        public PairedMarkupTagParser(List<IMarkupRule> currentMarkupRules)
        {
            CurrentMarkupRules = currentMarkupRules
                .Where(e => e.HaveClosingMarkupTag)
                .OrderByDescending(e => e.MarkupTag.Length)
                .ToList();
        }

        private List<IMarkupRule> CurrentMarkupRules { get; }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            for (var i = 0; i < line.Length; i++)
            {
                var rule = DetermineRule(line, i);
                if (rule == null) continue;

                if (Utils.CanBeOpenningTag(line, i))
                {
                    var subline = new ParsedSubline
                    {
                        LeftBorderOfSubline = i,
                        MarkupRule = rule
                    };
                    stack.Push(subline);
                }
                else if (Utils.CanBeClosingTag(line, i, rule.MarkupTag.Length))
                {
                    var element = GetClosingElement(stack, rule);
                    if (element == null) continue;
                    element.RightBorderOfSubline = i;
                    result.Add(element);
                }
                i += rule.MarkupTag.Length - 1;
            }
            return result;
        }

        private static ParsedSubline GetClosingElement(Stack<ParsedSubline> stack, IMarkupRule rule)
        {
            while (stack.Count > 0)
            {
                var element = stack.Pop();
                if (element.MarkupRule == rule)
                    return element;
            }
            return null;
        }

        private IMarkupRule DetermineRule(string line, int i)
        {

            return CurrentMarkupRules
                .Where(rule => i + rule.MarkupTag.Length <= line.Length)
                .FirstOrDefault(rule => line.Substring(i, rule.MarkupTag.Length) == rule.MarkupTag);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Markdown.Parsers;
using NUnit.Framework;

namespace Markdown
{
    public class TextParser
    {
        private List<IMarkupRule> CurrentMarkupRules { get; }
        public TextParser(List<IMarkupRule> rules)
        {
            CurrentMarkupRules = rules;
        }

        public IEnumerable<ParsedSubline> ParseLine(string line)
        {
            var result = new List<ParsedSubline>();
            var singleTagsParser = new SingleMarkupTagsParser();
            var pairTagsParser = new PairedMarkupTagParser();

            foreach (var currentMarkupRule in CurrentMarkupRules)
                result.AddRange(currentMarkupRule.HaveClosingMarkupTag
                    ? pairTagsParser.ParseLine(line, currentMarkupRule.MarkupTag)
                    : singleTagsParser.ParseLine(line, currentMarkupRule.MarkupTag));

            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Markdown.MarkupRules;
using Markdown.Rules;
using NUnit.Framework;

namespace Markdown
{
    public class TextParser
    {
        public List<MarkupRule> CurrentMarkupRules { get; }
        public TextParser(List<MarkupRule> rules)
        {
            CurrentMarkupRules = rules;
        }

        public IEnumerable<ParsedSubline> ParseLine(string s)
        {
            return CurrentMarkupRules
                .SelectMany(e => e.ParseLineWithRule(s));
            
        }

        private IEnumerable<(int, MarkupRule)> GetAllPotentialIndexiesOfMarkup(string s)
        {
            return CurrentMarkupRules
                .SelectMany(e => s.AllIndexesOf(e.MarkdownTag)
                    .Select(i => (i, e)));
        }
    }
}
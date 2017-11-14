using System.Collections.Generic;
using System.Linq;
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
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            var indexies = GetAllPotentialIndexiesOfMarkup(s);

            foreach (var index in indexies)
            {
                if (IsOpeningMarkupTag())
                {
                    
                }
            }
            return result;
        }

        private IEnumerable<(int, MarkupRule)> GetAllPotentialIndexiesOfMarkup(string s)
        {
            return CurrentMarkupRules
                .SelectMany(e => s.AllIndexesOf(e.MarkdownTag)
                    .Select(i => (i, e)));
        }
    }
}
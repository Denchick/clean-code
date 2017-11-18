using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    public class TextParser
    {
        public List<MarkupRule> CurrentMarkupRules { get; }
        public TextParser(List<MarkupRule> rules)
        {
            CurrentMarkupRules = rules;
        }

        public IEnumerable<ParsedSubline> PurseLine(string line)
        {
            return CurrentMarkupRules
                .SelectMany(e => e.ParseLine(line));
        }
    }
}
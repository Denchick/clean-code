using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public abstract class MarkupRule
    {
        public abstract string MarkupTag { get; }
        public abstract string HtmlTag { get; }

        public abstract IEnumerable<ParsedSubline> ParseLine(string line);
    }
}

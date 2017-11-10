using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.Rules
{
    internal class Cursive : Rule
    {
        public new string MarkdownTag { get; } = "_";
        public new string HtmlTag { get; } = "em";
        public new bool HaveClosingHtmlTag { get; } = true;
        public new bool HaveClosingMarkdownTag { get; } = true;
    }
}

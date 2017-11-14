using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.MarkupRules
{
    internal class Bold : MarkupRule
    {
        public new string MarkdownTag { get; } = "__";
        public new string HtmlTag { get; } = "strong";
        public new bool HaveClosingHtmlTag { get; } = true;
        public new bool HaveClosingMarkdownTag { get; } = true;
    }
}

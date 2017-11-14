using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class MarkupRule
    {
        public string MarkdownTag { get; }
        public string HtmlTag { get; }
        public bool HaveClosingHtmlTag { get; }
        public bool HaveClosingMarkdownTag { get; }
    }
}

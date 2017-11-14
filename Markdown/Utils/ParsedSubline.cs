using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class ParsedSubline
    {
        public int LeftBorderOfSubline { get; set; }
        public int RightBorderOfSubline { get; set; }
        public MarkupRule MarkupRule { get; set; }
    }

}

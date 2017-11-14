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
        public string MarkdownTag { get; }
        public string HtmlTag { get; }
        public bool HaveClosingHtmlTag { get; }
        public bool HaveClosingMarkdownTag { get; }

        public IEnumerable<ParsedSubline> ParseLineWithRule(string s)
        {
            if (HaveClosingHtmlTag && HaveClosingMarkdownTag)
                return ParseLineByRuleWhichHasClosingMarkupAndHtmlTags(s);
            
            return new List<ParsedSubline>();
        }
        
        protected bool IsMarkupTagEnd(string s, int i)
        {
            return i == s.Length - MarkdownTag.Length ||
                   char.IsPunctuation(Convert.ToChar(s[i + MarkdownTag.Length])) ||
                   char.IsWhiteSpace(Convert.ToChar(s[i + MarkdownTag.Length]));
        }

        protected static bool IsMarkupTagStart(string s, int i)
        {
            return i == 0 || char.IsPunctuation(Convert.ToChar(s[i - 1])) ||
                   char.IsWhiteSpace(Convert.ToChar(s[i - 1]));
        }
        
        private IEnumerable<ParsedSubline> ParseLineByRuleWhichHasClosingMarkupAndHtmlTags(string s)
        {
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            for (var i = 0; i < s.Length - MarkdownTag.Length; i++)
            {
                if (s.Substring(i, MarkdownTag.Length) != MarkdownTag) continue;
                
                var subline = new ParsedSubline();
                if (IsMarkupTagStart(s, i))
                {
                    subline.LeftBorderOfSubline = i;
                    subline.MarkupRule = this;
                    stack.Push(subline);
                }
                else if (IsMarkupTagEnd(s, i))
                {
                    var element = stack.Count > 0 ? stack.Pop() : null;
                    if (element == null) continue;
                    element.RightBorderOfSubline = i;
                    result.Add(element);
                }           
            }
            return result;
        }
    }
    
    
}

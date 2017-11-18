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
        public abstract string MarkdownTag { get; }
        public abstract string HtmlTag { get; }
        protected abstract bool HaveClosingHtmlTag { get; }
        protected abstract bool HaveClosingMarkdownTag { get; }

        //абстрактный класс содержит всю логику парсинга, а наследники определяют его параметры. Напрашивается декомпозиция и выделение парсера  
        public IEnumerable<ParsedSubline> ParseLineWithRule(string s)
        {
            if (HaveClosingHtmlTag && HaveClosingMarkdownTag)
                return ParseLineByRuleWhichHasClosingMarkupAndHtmlTags(s);
            
            return Enumerable.Empty<ParsedSubline>();
        }

        //s - ни о чем не говорящее название
        private IEnumerable<ParsedSubline> ParseLineByRuleWhichHasClosingMarkupAndHtmlTags(string s)
        {
            var result = new List<ParsedSubline>();
            var stack = new Stack<ParsedSubline>();
            for (var i = 0; i < s.Length - MarkdownTag.Length; i++)
            {
                if (s.Substring(i, MarkdownTag.Length) != MarkdownTag) continue;

                if (IsMarkupTagStart(s, i))
                {
                    var subline = new ParsedSubline
                    {
                        LeftBorderOfSubline = i,
                        // зачем тут this? внешнему миру пофиг на то, как правило парсится, ему важен только HtmlTag
                        MarkupRule = this
                    };
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

        private bool IsMarkupTagEnd(string s, int i)
        {
            return i == s.Length - MarkdownTag.Length
                   || char.IsPunctuation(s[i + MarkdownTag.Length])
                   || char.IsWhiteSpace(s[i + MarkdownTag.Length]);
        }


        private static bool IsMarkupTagStart(string s, int i)
        {
            return i == 0
                   || char.IsPunctuation(s[i - 1])
                   || char.IsWhiteSpace(s[i - 1]);
        }
    }
    
    
}

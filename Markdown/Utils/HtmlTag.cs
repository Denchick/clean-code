namespace Markdown
{
    public class ToHtmlTag
    {
        public string TagName { get; }
        public bool IsClosingTag { get; }

        public ToHtmlTag(string markupRuleHtmlTag, bool isClosingTag)
        {
            TagName = markupRuleHtmlTag;
            IsClosingTag = isClosingTag;
        }
    }
}
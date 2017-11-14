namespace Markdown
{
    public class ToHtmlTag
    {
        public ToHtmlTag(string markupRuleHtmlTag, bool isClosingTag)
        {
            TagName = markupRuleHtmlTag;
            IsClosingTag = isClosingTag;
        }

        public string TagName { get; set; }
        public bool IsClosingTag { get; set; }
    }
}
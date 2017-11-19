namespace Markdown
{
    public class ToHtmlTag
    {
        public string TagName { get; }
        public bool IsClosingTag { get; }
        public int LenghtOfReplacedTag { get; }

        public ToHtmlTag(string markupRuleHtmlTag, bool isClosingTag, int lenghtOfReplacedTag)
        {
            TagName = markupRuleHtmlTag;
            IsClosingTag = isClosingTag;
            LenghtOfReplacedTag = lenghtOfReplacedTag;
        }

    }
}
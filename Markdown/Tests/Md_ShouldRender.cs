using System.Collections.Generic;
using FluentAssertions;
using Markdown.MarkupRules;
using NUnit.Framework;

//тесты лучше хранить в отдельной директории
namespace Markdown
{
    //не хватает тестов
    [TestFixture]
    public class Md_ShouldRender
    {

        [TestCase("")]
        [TestCase("kek")]
        [TestCase("this next is real complex")]
        [TestCase("kek_cheburek")]
        [TestCase("_kek")]
        [TestCase("kek_")]
        [TestCase("ke___k")]
        [TestCase("#kek")]
        [TestCase("k#ek")]
        [TestCase("kek#")]
        public void CorrectMarkup_WhenNothingToMarkUp(string s)
        {
            var rules = GetAllAvalableRules();
            
            var md = new Md(rules);

            md.RenderToHtml(s).Should().Be(s);
        }
        
        
        private static IEnumerable<MarkupRule> GetAllAvalableRules()
        {
            return new List<MarkupRule>() { new Bold(), new Cursive(), new Header() };
        }
    }
}
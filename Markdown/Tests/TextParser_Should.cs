using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Markdown.MarkupRules;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class TextParser_Should
    {
        [TestCase(null, TestName = "when line is null")]
        [TestCase("", TestName = "when line is empty")]
        [TestCase(" ", TestName = "when line is whitespace")]
        [TestCase("kek", TestName = "when line is just one word")]
        [TestCase(" kek", TestName = "when whitespace before word")]
        [TestCase("_kek", TestName = "when closing tag does not have a pair")]
        [TestCase("kek_", TestName = "when closing tag does not have a pair")]
        [TestCase("kek ", TestName = "when whitespace after word")]
        [TestCase("just simple text", TestName = "when line is some words separated by spacies")]
        public void CorrectParsing_WhenNothingToParse(string line)
        {
            var rules = Utils.GetAllAvalableRules();
            
            var parser = new TextParser(rules);
            var result = parser.ParseLine(line);
            
            result.Should().HaveCount(0);
        }

        [TestCase("_kek_", "_", 0, 4)]
        [TestCase("__kek__", "__", 0, 5)]
        [TestCase("___kek__", "__", 0, 6)]
        [TestCase("__kek___", "__", 0, 5)]
        [TestCase("# kek", "#", 0, 5)]
        public void CorrectParsing_WhenOneTagInLine(string line, string markupTag, int leftParsedIndex, int rightParsedIndex)
        {
            var parser = new TextParser(Utils.GetAllAvalableRules());
            var result = parser.ParseLine(line);
            var rule = Utils.GetAllAvalableRules()
                .First(e => e.MarkupTag == markupTag);

            var expected = new ParsedSubline()
            {
                LeftBorderOfSubline = leftParsedIndex,
                RightBorderOfSubline = rightParsedIndex,
                MarkupRule = rule
            };
            result.Should().HaveCount(1);
            result.First().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CorrectParsing_WhenFewTagsInLine()
        {
            var line = "_a_ __b__";
            var parser = new TextParser(Utils.GetAllAvalableRules());
            var result = parser.ParseLine(line);

            var cursiveTag = new ParsedSubline()
            {
                LeftBorderOfSubline = 0,
                RightBorderOfSubline = 2,
                MarkupRule = new Cursive()
            };
            var boldTag = new ParsedSubline()
            {
                LeftBorderOfSubline = 4,
                RightBorderOfSubline = 7,
                MarkupRule = new Bold()
            };
            
            var expected = new List<ParsedSubline>() { cursiveTag, boldTag };
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CorrectParsing_WhenNestingTagsInLine()
        {
            var line = "# _a_";
            var parser = new TextParser(Utils.GetAllAvalableRules());
            var result = parser.ParseLine(line);

            var headerTag = new ParsedSubline()
            {
                LeftBorderOfSubline = 0,
                RightBorderOfSubline = line.Length,
                MarkupRule = new Header()
            };
            var boldTag = new ParsedSubline()
            {
                LeftBorderOfSubline = 2,
                RightBorderOfSubline = 4,
                MarkupRule = new Cursive()
            };
            
            var expected = new List<ParsedSubline>() { headerTag, boldTag };
            result.Should().BeEquivalentTo(expected);
        }
    }
}
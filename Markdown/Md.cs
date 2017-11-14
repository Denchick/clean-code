using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using NUnit.Framework;
using System.Text;
using FluentAssertions;

namespace Markdown
{
	public class Md
	{
		private List<MarkupRule> CurrentMarkupRules { get; }
			
		public Md(params MarkupRule[] rules)
		{
			CurrentMarkupRules = new List<MarkupRule>();
			CurrentMarkupRules.AddRange(rules);
		}

		public string RenderToHtml(string markdown)
		{
		    var result = new StringBuilder();
			var parser = new TextParser(CurrentMarkupRules);
			var render = new TextRender(CurrentMarkupRules);
		    foreach (var s in markdown.Split('\n'))
		    {
		        var parsed = parser.ParseLine(s);
		        var rendered = render.RenderLine(s, parsed);
		        result.Append(rendered);
		    }
		    return result.ToString();
		}

	}

	[TestFixture]
	public class Md_ShouldRender
	{
		
		[TestCase("", "")]
		public void CorrectMarkup(string source, string expected)
		{
			var md = new Md();
			
			var rendered = md.RenderToHtml(source);

			rendered.Should().Be(expected);
		}
		

	}
}
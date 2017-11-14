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
			
		public Md(IEnumerable<MarkupRule> rules)
		{
			CurrentMarkupRules = rules
				.Where(e => e?.MarkdownTag != null)
				.OrderBy(e => e.MarkdownTag.Length)
				.ToList();
		}

		public string RenderToHtml(string markdown)
		{
		    var result = new StringBuilder();
			var render = new TextRender(CurrentMarkupRules);
		    foreach (var s in markdown.Split('\n'))
		    {
		        var parsed = CurrentMarkupRules
			        .SelectMany(e => e.ParseLineWithRule(s));
		        var rendered = render.RenderLine(s, parsed);
		        result.Append(rendered);
		    }
		    return result.ToString();
		}

	}
}
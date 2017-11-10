using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Text;

namespace Markdown
{
	public class Md
	{
		public string RenderToHtml(string markdown)
		{
		    var result = new StringBuilder();
		    foreach (var s in markdown.Split('\n'))
		    {
		        var parsed = ParseLine();
		        var rendered = RenderLine(s, parsed);
		        result.Append(rendered);
		    }
		    return result.ToString();
		}

	    private List<ParsedSubline> ParseLine()
	    {
	        throw new NotImplementedException();
	    }

	    private string RenderLine(string s, List<ParsedSubline> parsed)
	    {
	        throw new NotImplementedException();
	    }
	}

    [TestFixture]
	public class Md_ShouldRender
	{
	}
}
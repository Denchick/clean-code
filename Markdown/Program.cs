using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Markdown.MarkupRules;

namespace Markdown
{
	class Program
	{
		private static void Main(string[] args)
		{
			var textFromFile = File.ReadAllText(@"input.txt");
			var markupRules = new List<IMarkupRule>() {new Bold(), new Cursive(), new Header()};
			var md = new Md(markupRules);
			var result = md.RenderToHtml(textFromFile);
			
			using (var sw = new StreamWriter(@"output.txt"))
			{
				sw.WriteLine(result);
			}
			
		}
	}
}

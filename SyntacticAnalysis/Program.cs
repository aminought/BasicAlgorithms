using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var syntacticAnalysis = new SyntacticAnalysis ();
			var expressions = new List<String> {
				"1+2",
				"1+2+3",
				"2*3",
				"1+2*3",
				"(1+2)*3",
				"1+(2*3)",
				"(1+2)*(3+4)/5",
			};
			expressions.ForEach (expression => {
				var syntaxTree = syntacticAnalysis.Analyze (expression);
				Visualiser.Visualize (syntaxTree);
			});
		}
	}
}

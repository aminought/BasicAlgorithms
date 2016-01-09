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
				"cos(60)",
				"cos(60+23)*43",
				"cos(60+54*3)/54-8*sin(10)",
				"10!",
				"cos(56!+34)",
				"cos(sin(tan(56!)+34)*12)"
			};
			expressions.ForEach (expression => {
				var syntaxTree = syntacticAnalysis.Analyze (expression);
				Console.WriteLine (expression);
				Visualiser.Visualize (syntaxTree);
			});
		}
	}
}

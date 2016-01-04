using System;

namespace SyntacticAnalysis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var syntacticAnalysis = new SyntacticAnalysis ();
			string expression = "4+(2+5)";
			var syntaxTree = syntacticAnalysis.Analyze (expression);
			Visualiser.Visualize (syntaxTree);
		}
	}
}

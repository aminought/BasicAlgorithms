using System;

namespace SyntacticAnalysis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var syntacticAnalysis = new SyntacticAnalysis ();
			string expression = "2*3*(9+2)+4*(5+10)";
			var syntaxTree = syntacticAnalysis.Analyze (expression);
			Visualiser.Visualize (syntaxTree);
		}
	}
}

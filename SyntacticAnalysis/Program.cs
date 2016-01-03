using System;

namespace SyntacticAnalysis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var syntacticAnalysis = new SyntacticAnalysis ();
			string expression = "523+cos(4)/3!*8-(234+2*(-2+4))";
			var syntaxTree = syntacticAnalysis.Analysis (expression);
			Console.WriteLine ("OK");
		}
	}
}

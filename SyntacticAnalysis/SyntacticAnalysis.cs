using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	public class SyntacticAnalysis
	{
		public SyntaxTree Analysis (string expression)
		{
			var syntaxTree = new SyntaxTree ();
			var lexicalAnalysis = new LexicalAnalysis ();
			var tokens = lexicalAnalysis.Analize (expression);
			return syntaxTree;
		}
	}
}


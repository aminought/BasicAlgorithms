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
			for (int i = 0; i < tokens.Count; ++i) {
				var token = tokens [i];
				if (token.TypeOfToken == TypeOfToken.Operand) {
					
				}
			}
			return syntaxTree;
		}
	}
}


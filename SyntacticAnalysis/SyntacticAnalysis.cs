using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	public class SyntacticAnalysis
	{
		public SyntaxTree Analysis (string expression)
		{
			var syntaxTree = new SyntaxTree ();
			var operatorPriority = new Dictionary<char, int> {
				{ '+', 0 },
				{ '-', 0 },
				{ '/', 1 },
				{ '*', 1 },
				{ '!', 2 }
			};
			for (int i = 0; i < expression.Length; ++i) {
				char currentChar = expression [i];
				string token = Char.ToString (currentChar);
				if (Char.IsDigit (currentChar)) {
					int j = i;
					while (j++ < expression.Length) {
						char nextChar = expression [j];
						if (Char.IsDigit (nextChar)) {
							token += nextChar;
						} else {
							i = --j;
							break;
						}
					}

					int operand = 0;
					bool isNumber = Int32.TryParse (token, out operand);
					Node node;
					if (isNumber) {
						node = new Node (TypeOfNode.Operand);
						node.Value = token;
					} else {
						
					}
				}
			}
			return syntaxTree;
		}
	}
}


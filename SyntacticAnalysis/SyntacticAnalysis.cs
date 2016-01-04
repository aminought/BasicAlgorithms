using System;
using System.Collections.Generic;
using SyntacticAnalysis.LexicalAnalysis;
using System.Linq;

namespace SyntacticAnalysis
{
	public class SyntacticAnalysis
	{
		public SyntaxTree Analyze (string expression)
		{
			var syntaxTree = new SyntaxTree ();
			var lexicalAnalysis = new LexicalAnalysis.LexicalAnalysis ();
			var tokens = lexicalAnalysis.Analyze (expression);
			Process (tokens, syntaxTree.HeadNode);
			return syntaxTree;
		}

		public int Process (List<Token> tokens, Node parent)
		{
			int countOfTokensProcessed = 0;

			var leftToken = tokens [countOfTokensProcessed++];
			Node left;

			if (leftToken.Value.Equals ("(")) {
				var partOfTokens = tokens
					.SkipWhile (t => t != tokens [1])
					.TakeWhile (t => !t.Value.Equals (")"))
					.ToList ();
				countOfTokensProcessed += Process (partOfTokens, parent) + 1;
				left = parent.Operands [0];
			} else {
				left = new Node (leftToken);
			}

			var middleToken = tokens [countOfTokensProcessed++];
			var oper = new Node (middleToken);
			parent.RemoveChild (left);
			oper.AddChild (left);
			parent.AddChild (oper);

			var rightToken = tokens [countOfTokensProcessed++];

			if (rightToken.Value.Equals ("(")) {
				var partOfTokens = tokens
					.SkipWhile (t => t != tokens [3])
					.TakeWhile (t => !t.Value.Equals (")"))
					.ToList ();
				countOfTokensProcessed += Process (partOfTokens, oper) + 1;
			} else {
				var right = new Node (rightToken);
				oper.AddChild (right);
			}

			if (countOfTokensProcessed < tokens.Count) {
				oper.RemoveChild (oper.Operands [1]);
				var partOfTokens = tokens.SkipWhile (t => t != rightToken).ToList ();
				Process (partOfTokens, oper);
			}

			return countOfTokensProcessed;
		}
	}
}


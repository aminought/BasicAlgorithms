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
			tokens = PreProcess (tokens);
			Visualiser.ShowTokens (tokens);
			Process (tokens, syntaxTree.HeadNode);
			return syntaxTree;
		}

		public List<Token> PreProcess (List<Token> tokens) // (2)*3*(9+2)+4*(5+10) => (((2)*3)*(9+2))+(4*(5+10))
		{
			var newTokens = new List<Token> ();
			for (int i = 0; i < tokens.Count; ++i) {
				var token = tokens [i];
				newTokens.Add (token);
				if (token.Value == "*" || token.Value == "/") {
					int j = newTokens.Count - 2;
					var stack = new Stack<string> ();
					while (j >= 0) {
						token = newTokens [j];
						if (token.Value.Equals (")")) {
							stack.Push (token.Value);
						} else if (token.Value.Equals ("(")) {
							stack.Pop ();
						} 
						if (stack.Count == 0) {
							newTokens.Insert (j, new Token () { TypeOfToken = TypeOfToken.LeftBracket, Value = "(" });
							break;
						}
						j--;
					}
					i++;
					while (i < tokens.Count) {
						token = tokens [i];
						newTokens.Add (token);
						if (token.Value.Equals ("(")) {
							stack.Push (token.Value);
						} else if (token.Value.Equals (")")) {
							stack.Pop ();
						} 
						if (stack.Count == 0) {
							newTokens.Add (new Token () { TypeOfToken = TypeOfToken.RightBracker, Value = ")" });
							break;
						}
						i++;
					}
				}
			}
			return newTokens;
		}

		public int Process (List<Token> tokens, Node parent)
		{
			int countOfTokensProcessed = 0;

			var leftToken = tokens [countOfTokensProcessed++];
			Node left;

			if (leftToken.Value.Equals ("(")) {
				var stack = new Stack<Token> ();
				int i;
				for (i = countOfTokensProcessed - 1; i < tokens.Count; ++i) {
					if (tokens [i].Value.Equals ("(")) {
						stack.Push (tokens [i]);
					} else if (tokens [i].Value.Equals (")")) {
						stack.Pop ();
						if (stack.Count == 0) {
							break;
						}
					}
				}
				var partOfTokens = tokens
					.SkipWhile (t => t != tokens [countOfTokensProcessed])
					.TakeWhile (t => t != tokens [i])
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
					.SkipWhile (t => t != tokens [countOfTokensProcessed])
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


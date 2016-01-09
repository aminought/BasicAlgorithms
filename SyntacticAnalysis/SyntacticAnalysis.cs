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
			Process (tokens, syntaxTree.HeadNode);
			return syntaxTree;
		}

		public List<Token> PreProcess (List<Token> tokens) // add brackets
		{
			var newTokens = new List<Token> ();
			for (int i = 0; i < tokens.Count; ++i) {
				var token = tokens [i];
				newTokens.Add (token);
				if (token.Value == "*" || token.Value == "/" || token.TypeOfToken == TypeOfToken.UnaryRightOperator
				    || token.Value == "+" || token.Value == "-") {
					int j = newTokens.Count - 2;
					var stack = new Stack<string> ();
					while (j >= 0) { // add left bracket
						token = newTokens [j];
						if (token.Value.Equals (")")) {
							stack.Push (token.Value);
						} else if (token.Value.Equals ("(")) {
							stack.Pop ();
						} 
						if (stack.Count == 0) {
							if (j - 1 >= 0 && newTokens [j - 1].TypeOfToken == TypeOfToken.UnaryLeftOperator) {
								j--;
							}
							newTokens.Insert (j, new Token () { TypeOfToken = TypeOfToken.LeftBracket, Value = "(" });
							break;
						}
						j--;
					}
					token = tokens [i];
					if (token.TypeOfToken == TypeOfToken.UnaryRightOperator) {
						newTokens.Add (new Token () { TypeOfToken = TypeOfToken.RightBracket, Value = ")" });
						continue;
					}
					i++;
					while (i < tokens.Count) { // add right bracket
						token = tokens [i];
						newTokens.Add (token);
						if (token.Value.Equals ("(")) {
							stack.Push (token.Value);
						} else if (token.Value.Equals (")")) {
							stack.Pop ();
						} 
						if (stack.Count == 0 && token.TypeOfToken != TypeOfToken.UnaryLeftOperator) {
							newTokens.Add (new Token () { TypeOfToken = TypeOfToken.RightBracket, Value = ")" });
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

			if (tokens.Count == 1) { // operand without operator
				parent.AddChild (new Node (tokens [0]));
				return ++countOfTokensProcessed;
			}

			var firstToken = tokens [countOfTokensProcessed++];
			Node firstNode = GetNode (tokens, parent, ref countOfTokensProcessed, countOfTokensProcessed - 1);

			if (countOfTokensProcessed < tokens.Count) {
				var secondToken = tokens [countOfTokensProcessed++];
				Node secondNode = null;
				Node thirdNode = null;

				if (firstNode.Token.TypeOfToken == TypeOfToken.Operand) {
					secondNode = GetNode (tokens, firstNode, ref countOfTokensProcessed, countOfTokensProcessed - 1);
					secondNode.AddChild (firstNode);
				} else if (firstNode.Token.TypeOfToken == TypeOfToken.UnaryLeftOperator) {
					secondNode = GetNode (tokens, firstNode, ref countOfTokensProcessed, countOfTokensProcessed - 1);
					secondNode.AddChild (firstNode);
					parent.AddChild (secondNode);
				} else if (firstNode.Token.TypeOfToken == TypeOfToken.UnaryRightOperator) {
					secondNode = GetNode (tokens, parent, ref countOfTokensProcessed, countOfTokensProcessed - 1);
					secondNode.AddChild (firstNode);
					parent.RemoveLastChild ();
					parent.AddChild (secondNode);
				} else if (firstNode.Token.TypeOfToken == TypeOfToken.BinaryOperator) {
					secondNode = GetNode (tokens, parent, ref countOfTokensProcessed, countOfTokensProcessed - 1);
					secondNode.AddChild (firstNode);
					parent.RemoveLastChild ();
					parent.AddChild (secondNode);
				}

				if (secondNode.Token.TypeOfToken == TypeOfToken.UnaryRightOperator) {
					parent.AddChild (secondNode);
				} else if (secondNode.Token.TypeOfToken == TypeOfToken.BinaryOperator) {
					var thirdToken = tokens [countOfTokensProcessed++];
					thirdNode = GetNode (tokens, secondNode, ref countOfTokensProcessed, countOfTokensProcessed - 1);
					secondNode.AddChild (thirdNode);
					parent.AddChild (secondNode);
				}
			} else {
				parent.AddChild (firstNode);
			}

			return countOfTokensProcessed;
		}

		Node GetNode (List<Token> tokens, Node parent, ref int countOfTokensProcessed, int startOfProcess)
		{
			Node node;
			if (tokens [startOfProcess].Value.Equals ("(")) {
				var stack = new Stack<Token> ();
				int i;
				for (i = startOfProcess; i < tokens.Count; ++i) {
					if (tokens [i].Value.Equals ("(")) {
						stack.Push (tokens [i]);
					} else if (tokens [i].Value.Equals (")")) {
						stack.Pop ();
						if (stack.Count == 0) {
							break;
						}
					}
				}
				int cop = startOfProcess + 1;
				var partOfTokens = tokens
					.SkipWhile (t => t != tokens [cop])
					.TakeWhile (t => t != tokens [i])
					.ToList ();
				countOfTokensProcessed += Process (partOfTokens, parent) + 1;
				node = parent.Operands.Where (n => n.Token.TypeOfToken != TypeOfToken.Operand).FirstOrDefault ();
				if (node == null) {
					node = parent.Operands [0];
				}
			} else {
				node = new Node (tokens [startOfProcess]);
				if (node.Token.TypeOfToken == TypeOfToken.UnaryLeftOperator) {
					node.AddChild (GetNode (tokens, node, ref countOfTokensProcessed, startOfProcess + 1));
					countOfTokensProcessed += 1;
				}
			}
			return node;
		}
	}
}


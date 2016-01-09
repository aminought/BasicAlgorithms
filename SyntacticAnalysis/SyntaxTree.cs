using System;
using System.Collections.Generic;
using SyntacticAnalysis.LexicalAnalysis;

namespace SyntacticAnalysis
{
	public class Node
	{
		public Token Token { get; set; }

		public Node Parent { get; set; }

		public List<Node> Operands { get; set; }

		int PossibleCount = 0;

		public Node (Token token)
		{
			Token = token;
			if (Token.TypeOfToken == TypeOfToken.BinaryOperator) {
				PossibleCount = 2;
			} else if (Token.TypeOfToken == TypeOfToken.UnaryLeftOperator
			           || Token.TypeOfToken == TypeOfToken.UnaryRightOperator
			           || token.TypeOfToken == TypeOfToken.Equals) {
				PossibleCount = 1;
			}
			if (PossibleCount > 0) {
				Operands = new List<Node> ();
			}
		}

		public bool AddChild (Node child)
		{
			if (Operands.Count < PossibleCount) {
				child.Parent = this;
				Operands.Add (child);
				return true;
			}
			return false;
		}

		public bool RemoveChild (Node child)
		{
			if (Operands.Count > 0) {
				child.Parent = null;
				Operands.Remove (child);
				return true;
			}
			return false;
		}

		public bool RemoveLastChild ()
		{
			if (Operands.Count > 0) {
				Operands.RemoveAt (Operands.Count - 1);
				return true;
			}
			return false;
		}
	}

	public class SyntaxTree
	{
		public Node HeadNode { get; set; }

		public SyntaxTree ()
		{
			HeadNode = new Node (new Token () { TypeOfToken = TypeOfToken.Equals, Value = "=" });
		}
	}
}


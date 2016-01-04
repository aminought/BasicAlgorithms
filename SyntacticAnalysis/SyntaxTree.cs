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

		public Node (Token token)
		{
			Token = token;
			if (token.TypeOfToken == TypeOfToken.BinaryOperator
			    || token.TypeOfToken == TypeOfToken.UnaryLeftOperator
			    || token.TypeOfToken == TypeOfToken.UnaryRightOperator
			    || token.TypeOfToken == TypeOfToken.Equals) {
				Operands = new List<Node> ();
			}
		}

		public void AddChild (Node child)
		{
			child.Parent = this;
			Operands.Add (child);
		}

		public void RemoveChild (Node child)
		{
			child.Parent = null;
			Operands.Remove (child);
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


using System;
using System.Collections.Generic;

namespace SyntacticAnalysis
{
	public enum TypeOfNode
	{
		Operand,
		Operator
	}

	public class Node
	{
		public string Value { get; set; }

		public TypeOfNode TypeOfNode { get; set; }

		public List<Node> Operands { get; set; }

		public Node (TypeOfNode typeOfNode)
		{
			if (typeOfNode == TypeOfNode.Operator) {
				Operands = new List<Node> ();
			}
		}
	}

	public class SyntaxTree
	{
		public List<Node> Nodes { get; set; }

		public SyntaxTree ()
		{
			Nodes = new List<Node> ();
		}

	}
}


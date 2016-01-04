using System;
using System.Collections.Generic;
using SyntacticAnalysis.LexicalAnalysis;

namespace SyntacticAnalysis
{
	public static class Visualiser
	{
		public static void Visualize (SyntaxTree SyntaxTree)
		{
			SyntaxTree.HeadNode.PrintPretty ("", true, false);
		}

		public static void ShowTokens (List<Token> tokens)
		{
			tokens.ForEach (t => Console.Write (t.Value + " "));
			Console.WriteLine ();
		}
	}

	public static class NodePrintExtension
	{
		public static void PrintPretty (this Node node, string indent, bool first, bool last)
		{
			Console.Write (indent);
			if (last) {
				Console.Write ("+- ");
				indent += "  ";
			} else if (!first) {
				Console.Write ("|- ");
				indent += "| ";
			}
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine (node.Token.Value);
			Console.ResetColor ();

			if (node.Operands != null) {
				for (int i = 0; i < node.Operands.Count; ++i) {
					node.Operands [i].PrintPretty (indent, false, i == node.Operands.Count - 1);
				}
			}
		}
	}
}

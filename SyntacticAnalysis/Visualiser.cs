using System;

namespace SyntacticAnalysis
{
	public static class Visualiser
	{
		public static void Visualize (SyntaxTree SyntaxTree)
		{
			SyntaxTree.HeadNode.PrintPretty ("", true, false);
		}
	}

	public static class NodePrintExtension
	{
		public static void PrintPretty (this Node node, string indent, bool first, bool last)
		{
			Console.Write (indent);
			if (last) {
				Console.Write ("\\-");
				indent += "  ";
			} else if (!first) {
				Console.Write ("|-");
				indent += "| ";
			}
			Console.WriteLine (node.Token.Value);

			if (node.Operands != null) {
				for (int i = 0; i < node.Operands.Count; ++i) {
					node.Operands [i].PrintPretty (indent, false, i == node.Operands.Count - 1);
				}
			}
		}
	}
}

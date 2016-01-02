using System;
using System.Collections.Generic;

namespace EightQueensPuzzle
{
	public static class Visualizer
	{
		public static void Visualize (List<Stack<Tuple<int, int>>> Saved, int size, bool show)
		{
			Console.WriteLine (Saved.Count + " solutions");
			Console.WriteLine ();

			if (show) {
				Saved.ForEach ((Solution) => {
					for (int i = 0; i < size; ++i) {
						for (int j = 0; j < size; ++j) {
							Console.Write (Solution.Contains (new Tuple<int, int> (i, j)) ? "1 " : "* ");
						}
						Console.WriteLine ();
					}
					Console.WriteLine ();
				});
			}
		}
	}
}


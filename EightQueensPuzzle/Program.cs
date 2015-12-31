using System;
using System.Diagnostics;

namespace EightQueensPuzzle
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const int size = 9;

			Stopwatch watch = Stopwatch.StartNew ();
			var Puzzle = new EightQueensPuzzle (size);
			Puzzle.process ();
			watch.Stop ();

			Visualizer.Visualize (Puzzle.Saved, size, false);
			Console.WriteLine (watch.ElapsedMilliseconds + " ms");
		}
	}
}

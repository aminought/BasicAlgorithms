using System;
using System.Collections.Generic;
using System.Threading;

namespace KnightTour
{
	public static class Visualizer
	{
		public static void Visualize (List<Step> steps, int size)
		{
			string[][] table = new string[size] [];
			for (int i = 0; i < size; ++i) {
				table [i] = new string[size];
				for (int j = 0; j < size; ++j) {
					table [i] [j] = "**";
				}
			}

			for (int i = 0; i < size * size; ++i) {
				string step = string.Format ("{0}", i + 1);
				if (step.Length == 1) {
					step = " " + step;
				}
				table [steps [i].I] [steps [i].J] = step;
				Console.Clear ();
				Show (table, size, step);
				Thread.Sleep (500);
				Console.WriteLine ();
			}
		}

		private static void Show (string[][] table, int size, string highlight)
		{
			for (int i = 0; i < size; ++i) {
				for (int j = 0; j < size; ++j) {
					if (table [i] [j] != "**") {
						Console.ForegroundColor = table [i] [j] == highlight ? ConsoleColor.Green : ConsoleColor.Red;
					}
					Console.Write (table [i] [j] + " ");
					Console.ResetColor ();
				}
				Console.WriteLine ("\n");
			}
		}
	}
}


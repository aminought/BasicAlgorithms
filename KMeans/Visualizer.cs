using System;
using System.Collections.Generic;

namespace KMeans
{
	public static class Visualizer
	{
		public static void Visualize (int width, int height, List<Point> points)
		{
			char[,] field = new char[width, height];
			for (int x = 0; x < width; ++x) {
				for (int y = 0; y < height; ++y) {
					Point point = points.Find ((p) => p.X == x && p.Y == y);
					if (point != null) {
						Console.ForegroundColor = ChooseColor (point.Cluster.Id);
						Console.Write ("*");
						Console.ResetColor ();
					} else {
						Console.Write (" ");
					}
				}
				Console.WriteLine ();
			}
		}

		public static ConsoleColor ChooseColor (int n)
		{
			Array colors = Enum.GetValues (typeof(ConsoleColor));
			return (ConsoleColor)colors.GetValue (n % (colors.Length - 1) + 1);
		}
	}
}


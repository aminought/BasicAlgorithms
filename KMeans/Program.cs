using System;
using System.Collections.Generic;

namespace KMeans
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			const int width = 50;
			const int height = 50;
			List<Tuple<int, int>> table = new List<Tuple<int, int>> ();
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					table.Add (new Tuple<int, int> (i, j));
				}
			}

			const int count_of_points = 2500;
			var points = new List<Point> ();
			var rand = new Random ();
			for (int i = 0; i < count_of_points; ++i) {
				Tuple<int, int> rand_tuple = table [rand.Next (table.Count)];
				var point = new Point (rand_tuple.Item1, rand_tuple.Item2);
				table.Remove (rand_tuple);
				points.Add (point);
			}

			const int count_of_custers = 100;
			var clusters = new List<Cluster> ();
			var old_mean_points = new List<Point> ();
			for (int i = 0; i < count_of_custers; ++i) {
				var random_mean_point = points [rand.Next (points.Count)];
				Cluster cluster = new Cluster (i, random_mean_point);
				cluster.Points.Add (random_mean_point);
				random_mean_point.Cluster = cluster;
				clusters.Add (cluster);
				old_mean_points.Add (random_mean_point);
			}

			bool isContinue = true;
			do {
				points.ForEach ((p) => {
					var distances = new SortedDictionary<double, Point> ();
					clusters.ForEach ((c) => {
						try {
							distances.Add (p.Distance (c.MeanPoint), c.MeanPoint);
						} catch (ArgumentException) {
							// Nothing to do
						}
					});
					SortedDictionary<double, Point>.ValueCollection.Enumerator enumerator = distances.Values.GetEnumerator ();
					enumerator.MoveNext ();
					p.Cluster = enumerator.Current.Cluster;
					p.Cluster.Points.Add (p);
				});

				var new_mean_points = new List<Point> ();
				clusters.ForEach ((c) => {
					int sumX = 0;
					int sumY = 0;
					c.Points.ForEach ((p) => {
						sumX += p.X;
						sumY += p.Y;
					});
					Point central_point = new Point (sumX / c.Points.Count, sumY / c.Points.Count);
					var distances = new SortedDictionary<double, Point> ();
					c.Points.ForEach ((p) => {
						try {
							distances.Add (p.Distance (central_point), p);
						} catch (ArgumentException) {
							// Nothing to do
						}
					});
					SortedDictionary<double, Point>.ValueCollection.Enumerator enumerator = distances.Values.GetEnumerator ();
					enumerator.MoveNext ();
					Point new_mean_point = enumerator.Current;
					c.MeanPoint = new_mean_point;
					new_mean_points.Add (new_mean_point);
				});
				visualize (width, height, points);
				Console.WriteLine ();

				for (int i = 0; i < clusters.Count; ++i) {
					if (!old_mean_points [i].Equals (new_mean_points [i])) {
						break;
					} else {
						isContinue = false;
					}
				} 
				if (isContinue == true) {
					old_mean_points = new_mean_points;
				}
			} while(isContinue);

			return 0;
		}

		public static void visualize (int width, int height, List<Point> points)
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

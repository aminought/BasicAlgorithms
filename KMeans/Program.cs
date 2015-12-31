using System;
using System.Collections.Generic;

namespace KMeans
{
	class KMeans
	{
		private List<Point> Points { get; set; }

		private List<Cluster> Clusters { get; set; }

		public static int Main (string[] args)
		{
			const int width = 50;
			const int height = 50;
			const int count_of_points = width * height;
			const int count_of_clusters = 10;

			var kmeans = new KMeans ();
			kmeans.process (width, height, count_of_points, count_of_clusters);
			Visualizer.Visualize (width, height, kmeans.Points);
				
			return 0;
		}

		public KMeans ()
		{
			Points = new List<Point> ();
			Clusters = new List<Cluster> ();
		}

		public void process (int width, int height, int count_of_points, int count_of_clusters)
		{
			var table = CreateTableForUniquePoints (width, height);
			GenerateRandomPoints (count_of_points, table);
			var old_mean_points = new List<Point> ();
			GenerateRandomClusters (count_of_clusters, old_mean_points);

			bool isContinue = true;
			do {
				SplitPointsIntoClusters ();
				var new_mean_points = new List<Point> ();
				ComputeNewMeanPoints (new_mean_points);

				for (int i = 0; i < Clusters.Count; ++i) {
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
		}

		private List<Tuple<int, int>> CreateTableForUniquePoints (int width, int height)
		{
			List<Tuple<int, int>> table = new List<Tuple<int, int>> ();
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					table.Add (new Tuple<int, int> (i, j));
				}
			}
			return table;
		}

		private void GenerateRandomPoints (int count_of_points, List<Tuple<int, int>> table)
		{
			var rand = new Random ();
			for (int i = 0; i < count_of_points; ++i) {
				Tuple<int, int> rand_tuple = table [rand.Next (table.Count)];
				var point = new Point (rand_tuple.Item1, rand_tuple.Item2);
				table.Remove (rand_tuple);
				Points.Add (point);
			}
		}

		private void GenerateRandomClusters (int count_of_clusters, List<Point> old_mean_points)
		{
			var rand = new Random ();
			for (int i = 0; i < count_of_clusters; ++i) {
				var random_mean_point = Points [rand.Next (Points.Count)];
				Cluster cluster = new Cluster (i, random_mean_point);
				cluster.Points.Add (random_mean_point);
				random_mean_point.Cluster = cluster;
				Clusters.Add (cluster);
				old_mean_points.Add (random_mean_point);
			}
		}

		private void SplitPointsIntoClusters ()
		{
			Points.ForEach (p => {
				var distances = new SortedDictionary<double, Point> ();
				Clusters.ForEach (c => {
					try {
						distances.Add (p.Distance (c.MeanPoint), c.MeanPoint);
					} catch (ArgumentException) {
						// Nothing to
					}
				});
				SortedDictionary<double, Point>.ValueCollection.Enumerator enumerator = distances.Values.GetEnumerator ();
				enumerator.MoveNext ();
				p.Cluster = enumerator.Current.Cluster;
				p.Cluster.Points.Add (p);
			});
		}

		private void ComputeNewMeanPoints (List<Point> new_mean_points)
		{
			Clusters.ForEach (c => {
				int sumX = 0;
				int sumY = 0;
				c.Points.ForEach (p => {
					sumX += p.X;
					sumY += p.Y;
				});
				Point central_point = new Point (sumX / c.Points.Count, sumY / c.Points.Count);
				var distances = new SortedDictionary<double, Point> ();
				c.Points.ForEach (p => {
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
		}
	}
}

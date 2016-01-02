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
			const int countOfPoints = width * height;
			const int countOfClusters = 10;

			var kmeans = new KMeans ();
			kmeans.process (width, height, countOfPoints, countOfClusters);
			Visualizer.Visualize (width, height, kmeans.Points);
				
			return 0;
		}

		public KMeans ()
		{
			Points = new List<Point> ();
			Clusters = new List<Cluster> ();
		}

		public void process (int width, int height, int countOfPoints, int countOfClusters)
		{
			var table = CreateTableForUniquePoints (width, height);
			GenerateRandomPoints (countOfPoints, table);
			var oldMeanPoints = new List<Point> ();
			GenerateRandomClusters (countOfClusters, oldMeanPoints);

			bool isContinue = true;
			do {
				SplitPointsIntoClusters ();
				var newMeanPoints = new List<Point> ();
				ComputeNewMeanPoints (newMeanPoints);

				for (int i = 0; i < Clusters.Count; ++i) {
					if (!oldMeanPoints [i].Equals (newMeanPoints [i])) {
						break;
					} else {
						isContinue = false;
					}
				} 
				if (isContinue == true) {
					oldMeanPoints = newMeanPoints;
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

		private void GenerateRandomPoints (int countOfPoints, List<Tuple<int, int>> table)
		{
			var rand = new Random ();
			for (int i = 0; i < countOfPoints; ++i) {
				Tuple<int, int> randTuple = table [rand.Next (table.Count)];
				var point = new Point (randTuple.Item1, randTuple.Item2);
				table.Remove (randTuple);
				Points.Add (point);
			}
		}

		private void GenerateRandomClusters (int countOfClusters, List<Point> oldMeanPoints)
		{
			var rand = new Random ();
			for (int i = 0; i < countOfClusters; ++i) {
				var randomMeanPoint = Points [rand.Next (Points.Count)];
				Cluster cluster = new Cluster (i, randomMeanPoint);
				cluster.Points.Add (randomMeanPoint);
				randomMeanPoint.Cluster = cluster;
				Clusters.Add (cluster);
				oldMeanPoints.Add (randomMeanPoint);
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

		private void ComputeNewMeanPoints (List<Point> newMeanPoints)
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
				Point newMeanPoint = enumerator.Current;
				c.MeanPoint = newMeanPoint;
				newMeanPoints.Add (newMeanPoint);
			});
		}
	}
}

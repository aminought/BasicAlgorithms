using System;
using System.Collections.Generic;

namespace KMeans
{
	public class Cluster
	{
		public List<Point> Points { get; set; }
		public Point MeanPoint { get; set; }
		public int Id { get; set; }

		public Cluster (int id, Point random_mean_point)
		{
			Points = new List<Point> ();
			Id = id;
			MeanPoint = random_mean_point;
		}
	}
}


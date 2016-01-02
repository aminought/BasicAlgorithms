using System;

namespace KMeans
{
	public class Point
	{
		public int X { get; set; }

		public int Y { get; set; }

		public Cluster Cluster { get; set; }

		public Point (int x, int y)
		{
			X = x;
			Y = y;
		}

		public double Distance (Point p)
		{
			return Math.Sqrt (Math.Pow (X - p.X, 2) + Math.Pow (Y - p.Y, 2));
		}

		public override bool Equals (object obj)
		{
			Point p = (Point)obj;
			return p.X == X && p.Y == Y;
		}

	}
}

using System;

namespace KnightTour
{
	public class Step
	{
		public int I { get; set; }

		public int J { get; set; }

		public Step (int i, int j)
		{
			I = i;
			J = j;
		}

		public override bool Equals (object obj)
		{
			Step other = (Step)obj;
			return other.I == I && other.J == J;
		}
	}
}


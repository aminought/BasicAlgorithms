using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightTour
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const int size = 8;
			var knightTour = new KnightTour ();
			List<Step> steps = knightTour.Process (size);
			Visualizer.Visualize (steps, size);
		}
	}
}

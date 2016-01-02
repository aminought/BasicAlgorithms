using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightTour
{
	public class KnightTour
	{
		public List<Step> Process (int size)
		{
			var steps = new List<Step> ();
			steps.Add (new Step (0, 0));
			while (steps.Count < size * size) {
				List<Step> possibleSteps = FindPossibleSteps (steps [steps.Count - 1], steps, size);
				Step nextStep = FindStepWithLeastPossibleSteps (possibleSteps, steps, size);
				steps.Add (nextStep);
			}
			return steps;
		}

		private List<Step> FindPossibleSteps (Step from, List<Step> steps, int size)
		{
			var allSteps = new List<Step> ();
			allSteps.Add (new Step (from.I - 2, from.J + 1));
			allSteps.Add (new Step (from.I - 1, from.J + 2));
			allSteps.Add (new Step (from.I + 1, from.J + 2));
			allSteps.Add (new Step (from.I + 2, from.J + 1));
			allSteps.Add (new Step (from.I + 2, from.J - 1));
			allSteps.Add (new Step (from.I + 1, from.J - 2));
			allSteps.Add (new Step (from.I - 1, from.J - 2));
			allSteps.Add (new Step (from.I - 2, from.J - 1));

			return allSteps.Where (s => {
				return s.I >= 0 && s.I < size && s.J >= 0 && s.J < size && !steps.Contains (s);
			}).ToList<Step> ();
		}

		private Step FindStepWithLeastPossibleSteps (List<Step> possibleSteps, List<Step> steps, int size)
		{
			return possibleSteps.OrderBy (s => FindPossibleSteps (s, steps, size).Count).First<Step> ();
		}
	}
}


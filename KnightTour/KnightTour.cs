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
				List<Step> possible_steps = FindPossibleSteps (steps [steps.Count - 1], steps, size);
				Step next_step = FindStepWithLeastPossibleSteps (possible_steps, steps, size);
				steps.Add (next_step);
			}
			return steps;
		}

		private List<Step> FindPossibleSteps (Step from, List<Step> steps, int size)
		{
			var all_steps = new List<Step> ();
			all_steps.Add (new Step (from.I - 2, from.J + 1));
			all_steps.Add (new Step (from.I - 1, from.J + 2));
			all_steps.Add (new Step (from.I + 1, from.J + 2));
			all_steps.Add (new Step (from.I + 2, from.J + 1));
			all_steps.Add (new Step (from.I + 2, from.J - 1));
			all_steps.Add (new Step (from.I + 1, from.J - 2));
			all_steps.Add (new Step (from.I - 1, from.J - 2));
			all_steps.Add (new Step (from.I - 2, from.J - 1));

			return all_steps.Where (s => {
				return s.I >= 0 && s.I < size && s.J >= 0 && s.J < size && !steps.Contains (s);
			}).ToList<Step> ();
		}

		private Step FindStepWithLeastPossibleSteps (List<Step> possible_steps, List<Step> steps, int size)
		{
			return possible_steps.OrderBy (s => FindPossibleSteps (s, steps, size).Count).First<Step> ();
		}
	}
}


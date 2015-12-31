using System;
using System.Collections.Generic;

namespace EightQueensPuzzle
{
	public class EightQueensPuzzle
	{
		public char[,] Desk { get; set; }

		public List<Stack<Tuple<int, int>>> Saved  { get; set; }

		private int size = 0;

		public EightQueensPuzzle (int size)
		{
			Desk = new char[size, size];
			Saved = new List<Stack<Tuple<int, int>>> ();
			this.size = size;
		}

		public void process ()
		{
			Stack<Tuple<int, int>> Solution = new Stack<Tuple<int, int>> ();
			for (int i = 0; i < size; ++i) {
				for (int j = 0; j < size; ++j) {
					Tuple<int, int> Tuple = new Tuple<int, int> (i, j);
					if (check (Solution, Tuple)) {
						Solution.Push (Tuple);
						if (Solution.Count == size) { // if solution found
							Saved.Add (new Stack<Tuple<int, int>> (Solution));
							var Last = Solution.Pop (); // search for next solution
							i = Last.Item1;
							j = Last.Item2;
						} else {
							break;
						}
					} 
					if (j == size - 1 && Solution.Count == i) { // if no solution for row
						var Last = Solution.Pop (); // backtrack 
						i = Last.Item1;
						j = Last.Item2;

						if (j == size - 1) { // if no solution for previous row
							if (Solution.Count == 0) { // if no other solutions
								return;
							}
							Last = Solution.Pop (); // backtrack one more time
							i = Last.Item1;
							j = Last.Item2;
						}
					}
				}
			}
		}

		public bool check (Stack<Tuple<int, int>> Saved, Tuple<int, int> Tuple)
		{
			if (Saved.Count == 0) {
				return true;
			}
			Tuple<int, int>[] SavedArr = Saved.ToArray ();
			for (int i = 0; i < SavedArr.Length; ++i) {
				int diffI = Math.Abs (SavedArr [i].Item1 - Tuple.Item1);
				int diffJ = Math.Abs (SavedArr [i].Item2 - Tuple.Item2);
				if (diffI == diffJ || SavedArr [i].Item2 == Tuple.Item2) {
					return false;
				}
			}
			return true;
		}
	}
}


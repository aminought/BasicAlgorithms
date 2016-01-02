using System;
using System.Linq;
using System.Collections.Generic;

namespace Bogosort
{
	public class Bogosort
	{
		public List<int> Sort (List<int> elements)
		{
			var sortedList = new List<int> (elements);
			var rand = new Random ();	
			while (!check (sortedList)) {
				sortedList = sortedList.OrderBy (e => rand.Next ()).ToList<int> ();
			}
			return sortedList;
		}

		private bool check (List<int> elements)
		{
			for (int i = 1; i < elements.Count; ++i) {
				int first = elements [i - 1];
				int second = elements [i];
				if (second < first) {
					return false;
				}
			}
			return true;
		}
	}
}


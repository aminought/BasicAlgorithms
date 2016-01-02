using System;
using System.Collections.Generic;

namespace SieveOfEratosthenes
{
	public class EratostheneBool : IEratosthene
	{
		public override void Sieve (int n)
		{
			var simpleBool = new List<bool> ();
			for (int i = 0; i < n; i++) {
				simpleBool.Add (true);
			}
			int currentIndex = 2;
			while (currentIndex < simpleBool.Count) {
				int p = currentIndex;
				if (simpleBool [p] == true) {
					for (int j = 2 * p; j < simpleBool.Count; j += p) {
						simpleBool [j] = false;
					}
				}
				currentIndex++;
			}
			for (int i = 2; i < simpleBool.Count; i++) {
				if (simpleBool [i] == true) {
					simple.Add (i);
				}
			}
		}
	}
}


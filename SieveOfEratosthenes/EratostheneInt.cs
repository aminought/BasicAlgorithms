using System;
using System.Collections.Generic;

namespace SieveOfEratosthenes
{
	public class EratostheneInt : IEratosthene
	{
		public override void Sieve (int n)
		{
			for (int i = 2; i <= n; i++) {
				simple.Add (i);
			}
			int currentIndex = 0;
			while (currentIndex < simple.Count) {
				int p = simple [currentIndex];
				if (p != 0) {
					for (int j = currentIndex + p; j < simple.Count; j += p) {
						simple [j] = 0;
					}
				}
				currentIndex++;
			}
			simple.RemoveAll ((e) => e == 0);
		}
	}
}


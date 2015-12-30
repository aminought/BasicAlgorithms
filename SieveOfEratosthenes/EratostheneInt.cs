using System;
using System.Collections.Generic;

namespace sieve_of_eratosthenes
{
	public class EratostheneInt : IEratosthene
	{
		public override void doErat (int n) {
			for (int i = 2; i <= n; i++) {
				simple.Add (i);
			}
			int current_index = 0;
			while(current_index < simple.Count) {
				int p = simple[current_index];
				if (p != 0) {
					for (int j = current_index + p; j < simple.Count; j += p) {
						simple [j] = 0;
					}
				}
				current_index++;
			}
			simple.RemoveAll ((e) => e == 0);
		}
	}
}


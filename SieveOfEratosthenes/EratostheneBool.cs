using System;
using System.Collections.Generic;

namespace sieve_of_eratosthenes
{
	public class EratostheneBool : IEratosthene
	{
		public override void doErat(int n) {
			var simpleBool = new List<bool>();
			for (int i = 0; i < n; i++) {
				simpleBool.Add (true);
			}
			int current_index = 2;
			while(current_index < simpleBool.Count) {
				int p = current_index;
				if (simpleBool[p] == true) {
					for (int j = 2*p; j < simpleBool.Count; j += p) {
						simpleBool [j] = false;
					}
				}
				current_index++;
			}
			for (int i = 2; i < simpleBool.Count; i++) {
				if (simpleBool [i] == true) {
					simple.Add (i);
				}
			}
		}
	}
}


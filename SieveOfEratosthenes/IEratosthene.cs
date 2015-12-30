using System;
using System.Collections.Generic;

namespace sieve_of_eratosthenes
{
	public abstract class IEratosthene
	{
		protected List<int> simple = new List<int> ();

		public abstract void doErat(int n);

		public void showResult() {
			simple.ForEach((e) => Console.Write (e + " "));
		}

		public List<int> getResult() {
			return simple;
		}
	}
}


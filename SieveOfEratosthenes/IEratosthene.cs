using System;
using System.Collections.Generic;

namespace SieveOfEratosthenes
{
	public abstract class IEratosthene
	{
		protected List<int> simple = new List<int> ();

		public abstract void Sieve(int n);

		public void ShowResult() {
			simple.ForEach((e) => Console.Write (e + " "));
		}

		public List<int> GetResult() {
			return simple;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SieveOfEratosthenes
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int n = int.Parse (Console.ReadLine ());

			var first = Stopwatch.StartNew ();
			IEratosthene eratostheneInt = new EratostheneInt ();
			eratostheneInt.Sieve (n);
			first.Stop ();
			eratostheneInt.ShowResult ();

			Console.WriteLine ();

			var second = Stopwatch.StartNew ();
			IEratosthene eratostheneBool = new EratostheneBool ();
			eratostheneBool.Sieve (n);
			second.Stop ();
			eratostheneBool.ShowResult ();

			Console.WriteLine ();
			Console.WriteLine ("First: " + first.ElapsedMilliseconds);
			Console.WriteLine ("Second: " + second.ElapsedMilliseconds);
		}
	}
}

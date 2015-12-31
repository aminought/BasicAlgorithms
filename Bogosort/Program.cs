using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosort
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var bogosort = new Bogosort ();
			var rand = new Random ();
			var elements = Enumerable.Range (0, 10).OrderBy (e => rand.Next ()).ToList<int> ();
			elements.ForEach (e => Console.Write (e + " "));
			elements = bogosort.Sort (elements);
			Console.WriteLine ();
			elements.ForEach (e => Console.Write (e + " "));
		}
	}
}

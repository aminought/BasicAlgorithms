using System;

namespace Fibonacci
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var fibonacciLoop = new FibonacciLoop ();
			var fibonacciMath = new FibonacciMath ();
			Console.WriteLine (fibonacciMath.Process (1000));
			Console.WriteLine (fibonacciLoop.Process (1000));
		}
	}
}

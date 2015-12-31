using System;

namespace Fibonacci
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var fibonacci_loop = new FibonacciLoop ();
			var fibonacci_math = new FibonacciMath ();
			Console.WriteLine (fibonacci_math.Process (1000));
			Console.WriteLine (fibonacci_loop.Process (1000));
		}
	}
}

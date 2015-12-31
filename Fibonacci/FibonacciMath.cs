using System;
using System.Numerics;

namespace Fibonacci
{
	public class FibonacciMath : IFibonacci
	{
		public BigInteger Process (int n)
		{
			return (BigInteger)Math.Round (Math.Pow ((1 + Math.Sqrt (5)) / 2, n) / Math.Sqrt (5));
		}
	}
}


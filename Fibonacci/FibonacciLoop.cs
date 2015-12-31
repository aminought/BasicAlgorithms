using System;
using System.Numerics;

namespace Fibonacci
{
	public class FibonacciLoop : IFibonacci
	{
		#region IFibonacci implementation

		public BigInteger Process (int n)
		{
			if (n == 1) {
				return 1;
			} else if (n == 2) {
				return 1;
			} else {
				BigInteger prev = 1;
				BigInteger sum = 1;

				for (int i = 3; i <= n; ++i) {
					BigInteger tmp = sum;
					sum += prev;
					prev = tmp;
				}
				return sum;
			}
		}

		#endregion
		
	}
}


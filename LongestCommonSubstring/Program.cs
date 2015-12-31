using System;

namespace MaximumRepetitiveSubstring
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var mrs = new MRS ();
			Console.WriteLine (mrs.Process ("abcdekjsdofijabcdesodifjabcoidof"));
		}
	}
}

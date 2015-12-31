using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumRepetitiveSubstring
{
	public class MRS
	{
		public string Process (string str)
		{
			char[][] matrix = new char[str.Length][];
			for (int i = 0; i < str.Length; ++i) {
				matrix [i] = new char[str.Length];
			}
			char[] strArr = str.ToCharArray ();

			for (int i = 0; i < str.Length; ++i) {
				for (int j = 0; j < str.Length; ++j) {
					if (strArr [i] == strArr [j]) {
						matrix [i] [j] = '1';
					}
				}
			}

			// ShowMatrix (matrix);

			var substrings = new List<string> ();
			for (int i = 0; i < str.Length; ++i) {
				String s = "";
				for (int j = 1; j + i < str.Length; ++j) {
					int x = j - 1;
					int y = j + i;
					char c = matrix [x] [y];
					if (c == '1') {
						s += strArr [y];
					} else {
						s += "*";
					}
				}
				if (s.Length != 0) {
					if (!substrings.Contains (s)) {
						var subs = new List<string> ();
						for (int k = 0; k < s.Length; k++) {
							string sub = "";
							while (k < s.Length && s [k] != '*') {
								sub += s [k++];
							}
							if (sub.Length != 0) {
								subs.Add (sub);
							}
						}
						if (subs.Count != 0) {
							substrings.Add (subs.OrderByDescending (e => e.Length).First ());
						}
					}
				}
			}

			return substrings.OrderByDescending (s => s.Length).First ();
		}

		private void ShowMatrix (char[][] matrix)
		{
			for (int i = 0; i < matrix.Length; ++i) {
				for (int j = 0; j < matrix [i].Length; ++j) {
					Console.Write (matrix [i] [j] == '1' ? '1' : '0');
				}
				Console.WriteLine ();
			}
		}
	}
}


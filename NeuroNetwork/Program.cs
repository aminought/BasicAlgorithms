using System;
using BitmapExtension;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace NeuroNetwork
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var pictureChooser = new PictureChooser (@"..\..\digits\");
			var pictures = pictureChooser.Pictures;
			var names = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
			var neuroNetwork = new NeuroNetwork (names, 30, 50);

			int i = 0;
			while (true) {
				Console.Clear ();
				Console.WriteLine (i++);

				var picturesList = pictures.ToList ();
				var rand = new Random ();
				var number = rand.Next (picturesList.Count);
				var picture = picturesList [number].Key;
				var name = picturesList [number].Value;
				int[,] input = picture.ToInput ();
				var neuroProb = neuroNetwork.Recognize (input);

				if (i < 2000) { // Auto learning
					bool isTrue = name.Equals (neuroProb.Key.Name);
					neuroProb.Key.Learn (isTrue, input);
				} else { // Manual learning
					ShowInput (input);
					Console.WriteLine ("Probably, it is " + neuroProb.Key.Name);
					Console.WriteLine ("Is it true?");
					var key = Console.ReadKey ();
					if (key.Key == ConsoleKey.UpArrow) {
						neuroProb.Key.Learn (true, input);
					} else if (key.Key == ConsoleKey.DownArrow) {
						neuroProb.Key.Learn (false, input); 
					}
				}
			}
		}

		public static void ShowInput (int[,] input)
		{
			for (int i = 0; i < input.GetLength (0); i += 2) {
				for (int j = 0; j < input.GetLength (1); j += 2) {
					Console.Write (input [i, j] == 1 ? "1" : " ");
				}
				Console.WriteLine ();
			}
			Console.WriteLine ();
		}
	}
}

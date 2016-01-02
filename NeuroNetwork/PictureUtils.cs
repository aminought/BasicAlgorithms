using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace NeuroNetwork
{
	public class PictureChooser
	{
		public Dictionary<Bitmap, string> Pictures { get; set; }

		public PictureChooser (string path)
		{
			Pictures = new Dictionary<Bitmap, string> ();
			var files = Directory.GetFiles (path);
			foreach (string file in files) {
				var picture = new Bitmap (file);
				var name = Path.GetFileNameWithoutExtension (file);
				Pictures.Add (picture, name);
			}
		}
	}
}

namespace BitmapExtension
{
	public static class BitmapExtension
	{
		public static int[,] ToInput (this Bitmap bitmap)
		{
			int[,] input = new int[bitmap.Height, bitmap.Width];
			for (int i = 0; i < bitmap.Width; ++i) {
				for (int j = 0; j < bitmap.Height; ++j) {
					var pixel = bitmap.GetPixel (i, j);
					input [j, i] = pixel.R == 255 ? 0 : 1;
				}
			}
			return input;
		}
	}
}


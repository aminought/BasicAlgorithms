using System;
using System.IO;

namespace NeuroNetwork
{
	public class Neuron
	{
		public string Name { get; set; }

		public int [,] Scale { get; set; }

		public int [,] Weight { get; set; }

		public int Width { get; set; } = 0;

		public int Height { get; set; } = 0;

		public Neuron (int width, int height, string name)
		{
			Width = width;
			Height = height;
			Name = name;
			Scale = new int[height, width];
			Weight = new int[height, width];
			ReadWeight ();
		}

		private void ReadWeight ()
		{
			var fileStream = new FileStream (Name, FileMode.OpenOrCreate);
			if (fileStream.Length != 0) {
				using (var br = new BinaryReader (fileStream)) {
					for (int i = 0; i < Height; ++i) {
						for (int j = 0; j < Width; ++j) {
							Weight [i, j] = br.ReadInt32 ();
						}
					}
				}
			} else {
				using (var bw = new BinaryWriter (fileStream)) {
					for (int i = 0; i < Height; ++i) {
						for (int j = 0; j < Width; ++j) {
							bw.Write (0);
						}
					}
				}
			}
			fileStream.Close ();
		}

		public int Recognize (int[,] input)
		{
			int probability = 0;
			for (int i = 0; i < Height; ++i) {
				for (int j = 0; j < Width; ++j) {
					Scale [i, j] = input [i, j] * Weight [i, j];
					probability += Scale [i, j];
				}
			}

			return probability;
		}

		public void Solution (bool isTrue, int[,] input)
		{
			var fileStream = new FileStream (Name, FileMode.Open);
			fileStream.SetLength (0);
			using (var br = new BinaryWriter (fileStream)) {
				for (int i = 0; i < Height; ++i) {
					for (int j = 0; j < Width; ++j) {
						Weight [i, j] += (isTrue ? 1 : (-1)) * input [i, j];
						br.Write (Weight [i, j]);
					}
				}
			}
			fileStream.Close ();
		}

	}
}


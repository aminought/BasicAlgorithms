using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroNetwork
{
	public class NeuroNetwork
	{
		public List<Neuron> Neurons { get; set; }

		public NeuroNetwork (List<string> names, int width, int height)
		{
			Neurons = new List<Neuron> (names.Count);
			names.ForEach (name => Neurons.Add (new Neuron (width, height, name)));
		}

		public KeyValuePair<Neuron, int> Recognize (int[,] input)
		{
			var mapping = new Dictionary<Neuron, int> ();
			Neurons.ForEach (neuron => {
				int probability = neuron.Recognize (input);
				mapping.Add (neuron, probability);
			});
			var probNeuron = mapping.OrderByDescending (pair => pair.Value).First ();
			return probNeuron;
		}
	}
}


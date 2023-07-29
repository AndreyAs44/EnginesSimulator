using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Simulator.ConfigurationLoaders.Engines
{
	public sealed record ParsingModel
	{
		[JsonPropertyName("I")]
		public double I { get; set; }

		[JsonPropertyName("M")]
		public List<double> M { get; set; }

		[JsonPropertyName("V")]
		public List<double> V { get; set; }

		[JsonPropertyName("T")]
		public double T { get; set; }

		[JsonPropertyName("Hm")]
		public double Hm { get; set; }

		[JsonPropertyName("Hv")]
		public double Hv { get; set; }

		[JsonPropertyName("C")]
		public double C { get; set; }
	}
}
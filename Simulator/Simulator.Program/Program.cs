using System;
using System.IO;
using Simulator.ConfigurationLoaders.Engines;
using Simulator.Engines.Engines;
using Simulator.Tests.Tests.Engines;

namespace Simulator
{
	internal static class Program
	{
		private static void Main()
		{
			// constants
			const string path = "../../../EngineConfiguration.json"; // path to engine configuration file
			const double timeStep = 1; // time step in seconds between each simulation step
			const double maxSimulationTime = 1000000; // maximum simulation time in seconds

			// input
			double environmentTemperature;
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Enter environment temperature:");

				var input = Console.ReadLine().Trim();
				if (double.TryParse(input, out environmentTemperature))
				{
					break;
				}
			}

			// load configuration from file
			var configurationLoader = new EngineConfigurationLoader(path);
			var engineConfiguration = configurationLoader.Load();
			
			// create engine
			var engine = new IcEngine(engineConfiguration, environmentTemperature);

			// run tests
			engine.Reset();
			var heatingTest = new HeatingTest<IcEngine>(engine, timeStep, maxSimulationTime);
			heatingTest.Run();

			engine.Reset();
			var maximumPowerTest = new MaximumPowerTest<IcEngine>(engine, timeStep);
			maximumPowerTest.Run();
		}
	}
}
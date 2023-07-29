using System;
using System.IO;
using System.Text.Json;
using Simulator.Engines.Engines.Configuration;

namespace Simulator.ConfigurationLoaders.Engines
{
	/// <summary>
	/// Loads engine configuration from json file
	/// </summary>
	public class EngineConfigurationLoader
	{
		/// <summary>
		/// Path to json file
		/// </summary>
		private readonly string _path;

		public EngineConfigurationLoader(string path)
		{
			_path = path;
		}

		/// <summary>
		/// Load engine configuration from json file and return it as EngineConfiguration
		/// </summary>
		/// <returns>Engine configuration from json file</returns>
		public EngineConfiguration Load()
		{
			// check if file exists
			if (!File.Exists(_path))
			{
				throw new FileNotFoundException("File not found", _path);
			}

			// load json file and deserialize it
			var json = File.ReadAllText(_path);
			
			// check if file is empty
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentException("File is empty", _path);
			}

			ParsingModel model;
			try
			{
				model = JsonSerializer.Deserialize<ParsingModel>(json);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			// create configuration from model
			var configuration = new EngineConfiguration
			{
				InertiaMoment = model.I,
				TorqueDependenceOfSpeed = new TorqueDependenceOfSpeed
				{
					TorqueArray = model.M.ToArray(),
					SpeedArray = model.V.ToArray()
				},
				OverheatTemperature = model.T,
				HeatingTorqueRate = model.Hm,
				HeatingSpeedRate = model.Hv,
				CoolingTemperatureRate = model.C
			};

			return configuration;
		}
	}
}
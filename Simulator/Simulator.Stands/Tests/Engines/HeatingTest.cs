using System;
using Simulator.Engines.Engines.Abstractions;
using Simulator.Tests.Tests.Engines.Abstractions;

namespace Simulator.Tests.Tests.Engines
{
	/// <summary>
	/// Test for checking engine overheating
	/// </summary>
	/// <typeparam name="TEngine">Engine physical model</typeparam>
	public class HeatingTest<TEngine> : BaseEngineTest<TEngine> where TEngine : Engine
	{
		/// <summary>
		/// Maximum simulation time
		/// </summary>
		private readonly double _simulationTime;

		/// <summary>
		/// Flag for checking if engine is overheated
		/// </summary>
		private bool _isOverheated;

		public HeatingTest(TEngine engine, double timeStep, double simulationTime) : base(engine, timeStep)
		{
			_simulationTime = simulationTime;
		}

		public override void Run()
		{
			// log
			Console.WriteLine("\nRunning heating test");

			// reset flag
			_isOverheated = false;

			// subscribe to events
			Simulator.OnSimulationStep += OnSimulationStep;
			Simulator.OnSimulationEnd += OnSimulationEnd;

			// start simulation
			Console.WriteLine($"Running simulation with time step {timeStep} and end time {_simulationTime}");
			Simulator.Run(timeStep, _simulationTime);
		}

		private void OnSimulationStep()
		{
			// check if engine is overheated
			var overheatedTemperature = engine.EngineConfiguration.OverheatTemperature;
			var currentTemperature = engine.CurrentTemperature;
			var isOverheated = currentTemperature > overheatedTemperature;
			if (isOverheated)
			{
				// stop simulation
				Simulator.Stop();

				// set flag
				_isOverheated = true;
			}
		}

		private void OnSimulationEnd()
		{
			// unsubscribe from events
			Simulator.OnSimulationStep -= OnSimulationStep;
			Simulator.OnSimulationEnd -= OnSimulationEnd;

			// output result
			if (_isOverheated)
			{
				Console.WriteLine($"Engine overheated at {Simulator.RunTime} seconds");
			}
			else
			{
				Console.WriteLine($"Engine did not overheat at {Simulator.RunTime} seconds");
			}
		}
	}
}
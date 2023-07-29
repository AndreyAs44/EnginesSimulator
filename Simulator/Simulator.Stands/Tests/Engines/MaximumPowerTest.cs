using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Engines.Engines.Abstractions;
using Simulator.Tests.Tests.Engines.Abstractions;

namespace Simulator.Tests.Tests.Engines
{
	public class MaximumPowerTest<TEngine> : BaseEngineTest<TEngine> where TEngine : Engine
	{
		/// <summary>
		/// List of power values for each simulation step
		/// </summary>
		private readonly List<double> _powerList = new();
		
		/// <summary>
		/// List of speed values for each simulation step
		/// </summary>
		private readonly List<double> _speedList = new();

		/// <summary>
		/// Previous step speed value
		/// </summary>
		private double _previousSpeed;

		public MaximumPowerTest(TEngine engine, double timeStep) : base(engine, timeStep)
		{
		}

		public override void Run()
		{
			// log
			Console.WriteLine("\nRunning maximum power test");

			// subscribe to events
			Simulator.OnSimulationStep += OnSimulationStep;
			Simulator.OnSimulationEnd += OnSimulationEnd;

			// start simulation
			Console.WriteLine($"Running simulation with time step {timeStep}");
			Simulator.Run(timeStep);
		}

		private void OnSimulationStep()
		{
			// add current power to list
			_speedList.Add(engine.CurrentSpeed);
			_powerList.Add(engine.CurrentPower);

			// check if speed is not change
			if (Math.Abs(engine.CurrentSpeed - _previousSpeed) < double.Epsilon)
			{
				// stop simulation
				Simulator.Stop();

				// output result (maximum power and speed for this power)
				var maxPower = _powerList.Max();
				var maxPowerIndex = _powerList.IndexOf(maxPower);
				var maxPowerSpeed = _speedList[maxPowerIndex];
				Console.WriteLine($"Maximum power: {maxPower} kW at {maxPowerSpeed} rad/s");
			}

			// save current speed
			_previousSpeed = engine.CurrentSpeed;
		}

		private void OnSimulationEnd()
		{
			// unsubscribe from events
			Simulator.OnSimulationStep -= OnSimulationStep;
			Simulator.OnSimulationEnd -= OnSimulationEnd;
		}
	}
}
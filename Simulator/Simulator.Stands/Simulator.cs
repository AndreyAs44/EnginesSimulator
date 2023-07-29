using System;
using Simulator.Engines.Common.Interfaces;

namespace Simulator.Tests
{
	/// <summary>
	/// Simulator class that runs a physical model
	/// </summary>
	/// <typeparam name="TPhysicalModel">Physical model that implements <see cref="IPhysicalModel" /></typeparam>
	public class Simulator<TPhysicalModel> where TPhysicalModel : IPhysicalModel
	{

		/// <summary>
		/// Flag to indicate if the simulation is running
		/// </summary>
		private bool _isRunning;

		public Simulator(TPhysicalModel physicalModel)
		{
			PhysicalModel = physicalModel;
		}

		/// <summary>
		/// The physical model to simulate
		/// </summary>
		public TPhysicalModel PhysicalModel { get; }

		/// <summary>
		/// The current simulation time
		/// </summary>
		public double RunTime { get; private set; }

		/// <summary>
		/// Event that is fired before the simulation starts
		/// </summary>
		public event Action OnSimulationStart;

		/// <summary>
		/// Event that is fired after each simulation step
		/// </summary>
		public event Action OnSimulationStep;

		/// <summary>
		/// Event that is fired after the simulation ends
		/// </summary>
		public event Action OnSimulationEnd;

		/// <summary>
		/// Runs the simulation until the end time is reached or the simulation is stopped
		/// </summary>
		/// <param name="timeStep">Time step between physical model simulations (in seconds)</param>
		/// <param name="endTime">End simulation after timeout (in seconds)</param>
		public void Run(double timeStep, double endTime = double.PositiveInfinity)
		{
			// set flag to true (running)
			_isRunning = true;

			// reset simulation time
			RunTime = 0;

			// fire simulation start event
			OnSimulationStart?.Invoke();

			// run simulation
			while (_isRunning && RunTime < endTime)
			{
				PhysicalModel.Simulate(timeStep);
				RunTime += timeStep;
				OnSimulationStep?.Invoke();
			}

			// fire simulation end event
			OnSimulationEnd?.Invoke();

			// reset flag
			_isRunning = false;
		}

		/// <summary>
		/// Stop the simulation
		/// </summary>
		public void Stop()
		{
			_isRunning = false;
		}
	}
}
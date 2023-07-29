using Simulator.Engines.Common.Interfaces;
using Simulator.Engines.Engines.Configuration;

namespace Simulator.Engines.Engines.Abstractions
{
	/// <summary>
	/// Base class for all engines models
	/// </summary>
	public abstract class Engine : IPhysicalModel
	{
		/// <summary>
		/// Temperature of environment (in Celsius)
		/// </summary>
		protected readonly double environmentTemperature;

		protected Engine(EngineConfiguration configuration, double environmentTemperature)
		{
			EngineConfiguration = configuration;
			this.environmentTemperature = environmentTemperature;

			CurrentTemperature = environmentTemperature;
		}

		/// <summary>
		/// Engine configuration (inertia moment, torque dependence of speed, etc.)
		/// </summary>
		public EngineConfiguration EngineConfiguration { get; }

		/// <summary>
		/// Current simulation time step (in seconds)
		/// </summary>
		public double CurrentTimeStep { get; private set; }

		/// <summary>
		/// Current engine speed (in rad/s)
		/// </summary>
		public double CurrentSpeed { get; private set; }

		/// <summary>
		/// Current engine torque (in N*m)
		/// </summary>
		public double CurrentTorque { get; private set; }

		/// <summary>
		/// Current engine acceleration (in rad/s^2)
		/// </summary>
		public double CurrentAcceleration { get; private set; }

		/// <summary>
		/// Current engine temperature (in Celsius)
		/// </summary>
		public double CurrentTemperature { get; private set; }

		/// <summary>
		/// Current engine power (in kW)
		/// </summary>
		public double CurrentPower { get; private set; }

		/// <summary>
		/// Simulate engine for specified time step
		/// </summary>
		/// <param name="timeStep">Time step (in seconds)</param>
		public virtual void Simulate(double timeStep)
		{
			// update time step before calculations
			CurrentTimeStep = timeStep;

			// steps of calculations (in order)
			CurrentTorque = CalculateTorque();
			CurrentAcceleration = CalculateAcceleration();
			CurrentSpeed = CalculateSpeed();
			CurrentTemperature = CalculateTemperature();
			CurrentPower = CalculatePower();
		}

		/// <summary>
		/// Reset engine to initial state
		/// </summary>
		public virtual void Reset()
		{
			CurrentTimeStep = 0;
			CurrentSpeed = 0;
			CurrentTorque = 0;
			CurrentAcceleration = 0;
			CurrentTemperature = environmentTemperature;
			CurrentPower = 0;
		}

		/// <summary>
		/// Calculate engine acceleration (in rad/s^2)
		/// </summary>
		/// <returns>New current engine acceleration (in rad/s^2)</returns>
		protected abstract double CalculateAcceleration();

		/// <summary>
		/// Calculate engine speed (in rad/s)
		/// </summary>
		/// <returns>New current engine speed (in rad/s)</returns>
		protected abstract double CalculateSpeed();

		/// <summary>
		/// Calculate engine torque (in N*m)
		/// </summary>
		/// <returns>New current engine torque (in N*m)</returns>
		protected abstract double CalculateTorque();

		/// <summary>
		/// Calculate engine temperature (in Celsius)
		/// </summary>
		/// <returns>New current engine temperature (in Celsius)</returns>
		protected abstract double CalculateTemperature();

		/// <summary>
		/// Calculate engine power (in kW)
		/// </summary>
		/// <returns>New current engine power (in kW)</returns>
		protected abstract double CalculatePower();

		public override string ToString()
		{
			return $"Speed: {CurrentSpeed}\n" +
			       $"Torque: {CurrentTorque}\n" +
			       $"Acceleration: {CurrentAcceleration}\n" +
			       $"Temperature: {CurrentTemperature}\n" +
			       $"Power: {CurrentPower}";
		}
	}
}
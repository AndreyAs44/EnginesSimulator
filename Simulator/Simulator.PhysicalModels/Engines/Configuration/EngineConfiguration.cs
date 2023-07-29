namespace Simulator.Engines.Engines.Configuration
{
	/// <summary>
	/// Engine configuration (inertia moment, torque dependence of speed, etc.)
	/// </summary>
	public sealed record EngineConfiguration
	{
		/// <summary>
		/// Inertia moment of engine (in kg*m^2)
		/// </summary>
		public double InertiaMoment { get; init; }

		/// <summary>
		/// Torque dependence of speed (in N*m, rad/s)
		/// </summary>
		public TorqueDependenceOfSpeed TorqueDependenceOfSpeed { get; init; }

		/// <summary>
		/// Temperature of engine overheating (in Celsius)
		/// </summary>
		public double OverheatTemperature { get; init; }

		/// <summary>
		/// Heating rate by torque (in t/(s*N*m))
		/// </summary>
		public double HeatingTorqueRate { get; init; }

		/// <summary>
		/// Heating rate by speed (in t*s/rad^2)
		/// </summary>
		public double HeatingSpeedRate { get; init; }

		/// <summary>
		/// Cooling rate (in 1/s)
		/// </summary>
		public double CoolingTemperatureRate { get; init; }
	}
}
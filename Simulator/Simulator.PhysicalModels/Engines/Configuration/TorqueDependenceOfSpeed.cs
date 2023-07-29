using Simulator.Engines.Common.Math;

namespace Simulator.Engines.Engines.Configuration
{
	/// <summary>
	/// Torque dependence of speed (in N*m, rad/s)
	/// </summary>
	public sealed class TorqueDependenceOfSpeed
	{
		/// <summary>
		/// Speed array (in rad/s)
		/// </summary>
		public double[] SpeedArray { get; init; }

		/// <summary>
		/// Torque array (in N*m)
		/// </summary>
		public double[] TorqueArray { get; init; }

		/// <summary>
		/// Get torque by speed (in N*m)
		/// </summary>
		/// <param name="speed">Speed within speed array (in rad/s)</param>
		/// <returns>Torque (in N*m)</returns>
		public double GetTorque(double speed)
		{
			return Interpolation.LinearByArray(speed, SpeedArray, TorqueArray);
		}
	}
}
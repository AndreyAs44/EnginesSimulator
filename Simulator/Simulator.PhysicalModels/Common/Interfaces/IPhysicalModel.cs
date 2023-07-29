namespace Simulator.Engines.Common.Interfaces
{
	/// <summary>
	/// Base interface for all physical models
	/// </summary>
	public interface IPhysicalModel
	{
		/// <summary>
		/// Simulate engine for specified time step
		/// </summary>
		/// <param name="timeStep">Time step in seconds</param>
		public void Simulate(double timeStep);

		/// <summary>
		/// Reset engine to initial state
		/// </summary>
		public void Reset();
	}
}
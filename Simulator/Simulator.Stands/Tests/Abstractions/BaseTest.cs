using Simulator.Engines.Common.Interfaces;

namespace Simulator.Tests.Tests.Abstractions
{
	public abstract class BaseTest<TPhysicalModel> where TPhysicalModel : IPhysicalModel
	{

		protected BaseTest(TPhysicalModel physicalModel)
		{
			Simulator = new Simulator<TPhysicalModel>(physicalModel);
		}

		/// <summary>
		/// Simulator class that runs a physical model
		/// </summary>
		protected Simulator<TPhysicalModel> Simulator { get; }

		/// <summary>
		/// Runs the simulation until the end time is reached or the simulation is stopped
		/// </summary>
		public abstract void Run();

		/// <summary>
		/// Stops the simulation
		/// </summary>
		public void Stop()
		{
			Simulator.Stop();
		}
	}
}
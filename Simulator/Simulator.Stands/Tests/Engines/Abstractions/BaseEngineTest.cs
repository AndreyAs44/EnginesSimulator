using Simulator.Engines.Engines.Abstractions;
using Simulator.Tests.Tests.Abstractions;

namespace Simulator.Tests.Tests.Engines.Abstractions
{
	/// <summary>
	/// Base class for engine tests
	/// </summary>
	/// <typeparam name="TEngine"></typeparam>
	public abstract class BaseEngineTest<TEngine> : BaseTest<TEngine> where TEngine : Engine
	{

		/// <summary>
		/// The engine to test
		/// </summary>
		protected readonly Engine engine;
		
		/// <summary>
		/// Time step between physical model simulations (in seconds)
		/// </summary>
		protected readonly double timeStep;

		protected BaseEngineTest(TEngine engine, double timeStep) : base(engine)
		{
			this.timeStep = timeStep;
			this.engine = engine;
		}
	}
}
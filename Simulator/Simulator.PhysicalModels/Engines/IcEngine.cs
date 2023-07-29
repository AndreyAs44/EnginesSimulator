using System;
using Simulator.Engines.Engines.Abstractions;
using Simulator.Engines.Engines.Configuration;

namespace Simulator.Engines.Engines
{
	/// <summary>
	/// Internal combustion engine (ICE) model
	/// </summary>
	public sealed class IcEngine : Engine
	{
		/// <summary>
		/// Rate of watts to kilowatts
		/// </summary>
		private const double WToKwRate = 1000;

		public IcEngine(EngineConfiguration configuration, double initialTemperature)
			: base(configuration, initialTemperature)
		{
		}

		protected override double CalculateAcceleration()
		{
			// calculate acceleration by torque and inertia moment
			var acceleration = CurrentTorque / EngineConfiguration.InertiaMoment;
			return acceleration;
		}

		protected override double CalculateSpeed()
		{
			// calculate speed by acceleration
			var speed = CurrentSpeed + CurrentAcceleration * CurrentTimeStep;
			return speed;
		}

		protected override double CalculateTorque()
		{
			// calculate torque by speed
			var torque = EngineConfiguration.TorqueDependenceOfSpeed.GetTorque(CurrentSpeed);
			return torque;
		}

		protected override double CalculateTemperature()
		{
			// calculate heating speed
			var torqueHeating = CurrentTorque * EngineConfiguration.HeatingTorqueRate;
			var speedHeating = Math.Pow(CurrentSpeed, 2) * EngineConfiguration.HeatingSpeedRate;
			var heatingSpeed = torqueHeating + speedHeating;

			// calculate cooling speed
			var deltaTemperature = environmentTemperature - CurrentTemperature;
			var coolingSpeed = EngineConfiguration.CoolingTemperatureRate * deltaTemperature;

			// calculate temperature
			var temperature = CurrentTemperature;
			temperature += heatingSpeed * CurrentTimeStep;
			temperature += coolingSpeed * CurrentTimeStep;

			return temperature;
		}

		protected override double CalculatePower()
		{
			// calculate power by torque and speed (in kW)
			var power = CurrentTorque * CurrentSpeed / WToKwRate;
			return power;
		}
	}
}
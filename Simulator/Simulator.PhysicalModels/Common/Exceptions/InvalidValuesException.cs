using System;
using System.Runtime.Serialization;

namespace Simulator.Engines.Common.Exceptions
{
	/// <summary>
	/// Exception for invalid values in functions
	/// </summary>
	[Serializable]
	public sealed class InvalidValuesException : Exception
	{
		private const string DefaultMessage = "Values is invalid";

		public InvalidValuesException() : base(DefaultMessage)
		{

		}

		public InvalidValuesException(string message) : base(message)
		{

		}

		public InvalidValuesException(string message, Exception innerException) : base(message, innerException)
		{

		}

		public InvalidValuesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{

		}
	}
}
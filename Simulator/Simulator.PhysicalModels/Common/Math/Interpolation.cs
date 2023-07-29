using Simulator.Engines.Common.Exceptions;

namespace Simulator.Engines.Common.Math
{
	public static class Interpolation
	{
		/// <summary>
		/// Linear interpolation between two points by arrays
		/// </summary>
		/// <param name="x">Point to interpolate</param>
		/// <param name="xArray">Base points (Ascending)</param>
		/// <param name="yArray">Function values for base points</param>
		/// <returns></returns>
		/// <exception cref="InvalidValuesException"></exception>
		public static double LinearByArray(double x, double[] xArray, double[] yArray)
		{
			// check if arrays are of the same length and throw exception if not
			if (xArray.Length != yArray.Length)
			{
				throw new InvalidValuesException("Arrays must be of the same length");
			}

			// check if x is in range of xArray
			if (x < xArray[0])
			{
				return yArray[0];
			}

			if (x > xArray[^1])
			{
				return yArray[xArray.Length - 1];
			}

			// find x in xArray, where xArray[i] <= x <= xArray[i + 1]. Then interpolate
			var length = xArray.Length;
			for (var i = 0; i < length - 1; i++)
			{
				if (x >= xArray[i] && x <= xArray[i + 1])
				{
					return Linear(x, xArray[i], xArray[i + 1], yArray[i], yArray[i + 1]);
				}
			}

			// if x is not in range of xArray, throw exception
			throw new InvalidValuesException("x is not in range of xArray");
		}

		/// <summary>
		/// Linear interpolation between two points
		/// </summary>
		/// <param name="x">Point to interpolate</param>
		/// <param name="x1">Base point for y1</param>
		/// <param name="x2">Base point for y2</param>
		/// <param name="y1">Function value at base point x1</param>
		/// <param name="y2">Function value at base point x2</param>
		/// <returns>Interpolated function value at point x</returns>
		public static double Linear(double x, double x1, double x2, double y1, double y2)
		{
			// check if x1 < x2 and throw exception if not
			if (x1 > x2)
			{
				throw new InvalidValuesException("x1 must be less than x2");
			}

			return y1 + (y2 - y1) * (x - x1) / (x2 - x1);
		}
	}
}
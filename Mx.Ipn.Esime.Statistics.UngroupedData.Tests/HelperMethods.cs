//TODO: clean UnitTests
namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public static class HelperMethods<T>
	{
		private static Random rnd;

		static HelperMethods ()
		{
			rnd = new Random (DateTime.Now.Millisecond);
		}

		public static IEnumerable<double> GetRandomDataSample (int size)
		{
			for (int i = 1; i <= size; i++) {
				yield return rnd.Next (57, 180) + Math.Round (rnd.NextDouble (), 2);
			}
		}

		public static dynamic NewInstance (out List<double> data, int size)
		{
			data = HelperMethods<T>.GetRandomDataSample (size).ToList ();
			var cache = data.ToList ();
			var calculator = NewInstance (ref cache);
			data = cache;
			
			return calculator;
		}

		public static dynamic NewInstance (ref List<double> data)
		{
			var calculator = (T)Activator.CreateInstance (typeof(T), new object[]{data.ToList ()});
			data.Sort ();
			
			return calculator;
		}

		public static double CalcNthXile (IList<double> data, int xile, int nTh)
		{
			var lx = data.Count * nTh / (double)xile;
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);
			if (ls == data.Count) {
				ls = li;
			}
			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;
			var xRange = iPortion * data [li] + sPortion * data [ls];
			
			return xRange;
		}
	}
}
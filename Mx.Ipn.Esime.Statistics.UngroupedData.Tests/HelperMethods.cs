namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public static class HelperMethods
	{
		private static Random rnd;

		static HelperMethods ()
		{
			rnd = new Random (DateTime.Now.Millisecond);
		}

		public static IEnumerable<double> GetRandomDataSample (int size)
		{
			for (int i = 0; i < size; i++) {
				yield return rnd.Next ();
			}
		}

		public static T NewInstanceOf <T> (out List<double> data, int size)
		{
			data = HelperMethods.GetRandomDataSample (size).ToList ();
			var cache = data.ToList ();
			var calculator = NewInstanceOf<T> (ref cache);
			data = cache;
			
			return calculator;
		}

		public static T NewInstanceOf <T> (ref List<double> data)
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
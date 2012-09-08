namespace Mx.Ipn.Esime.Statistics.Tests
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
			var rCalc = NewInstanceOf<T> (ref cache);
			data = cache;
			
			return rCalc;
		}

		public static T NewInstanceOf <T> (ref List<double> data)
		{
			var rCalc = (T)Activator.CreateInstance (typeof(T), new object[]{data.ToList ()});
			data.Sort ();
			
			return rCalc;
		}
	}
}


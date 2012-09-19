namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public abstract class HelperMethodsBase<T> where T:InquirerBase
	{
		private static Random rnd;

		static HelperMethodsBase ()
		{
			rnd = new Random (DateTime.Now.Millisecond);
		}

		public IEnumerable<double> GetRandomDataSample (int size)
		{
			for (int i = 1; i <= size; i++) {
				yield return rnd.Next (57, 180) + Math.Round (rnd.NextDouble (), 2);
			}
		}

		public T NewInquirer (int size)
		{
			List<double> data;
			return NewInquirer (out data, size);
		}

		public T NewInquirer (out List<double> data, int size)
		{
			data = GetRandomDataSample (size).ToList ();
			var cache = data.ToList ();
			var calculator = NewInquirer (ref cache);
			data = cache;
			
			return calculator;
		}

		public T NewInquirer (ref List<double> data)
		{
			var calculator = (T)Activator.CreateInstance (typeof(T), new object[]{data.ToList ()});
			data.Sort ();
			
			return calculator;
		}
					
		public abstract double CalcNthXile (IList<double> data, int xile, int nTh);
		
		public abstract double SampleMean (List<double> sortedData);
	}
}
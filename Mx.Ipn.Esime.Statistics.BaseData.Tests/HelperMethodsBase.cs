namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public abstract class HelperMethodsBase
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

		public TInquirer NewInquirer<TInquirer> (int size) where TInquirer:InquirerBase
		{
			List<double> data;
			return NewInquirer<TInquirer> (out data, size);
		}

		public TInquirer NewInquirer<TInquirer> (out List<double> data, int size) where TInquirer:InquirerBase
		{
			data = GetRandomDataSample (size).ToList ();
			var cache = data.ToList ();
			var calculator = NewInquirer<TInquirer> (cache);
			data = cache;
			
			return calculator;
		}

		public TInquirer NewInquirer<TInquirer> (List<double> data) where TInquirer:InquirerBase
		{
			try {
				var ctor = typeof(TInquirer).GetConstructor (new []{typeof(List<double>)});
				var calculator = (TInquirer)ctor.Invoke (new object[]{data != null ? data.ToList () : data});
				data.Sort ();

				return calculator;
			} catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
					
		public abstract double CalcNthXile (IList<double> data, int xile, int nTh);
		
		public abstract double SampleMean (List<double> sortedData);
	}
}
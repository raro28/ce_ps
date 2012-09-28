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

			var inquirer = NewInquirer<TInquirer> (GetRandomDataSample (size).ToList ());
			
			return inquirer;
		}

		public TInquirer NewInquirer<TInquirer> (List<double> data, params object[] args) where TInquirer:InquirerBase
		{
			try {
				var cacheArgs = args.ToList ();
				cacheArgs.Insert (0, data);

				var ctor = typeof(TInquirer).GetConstructors ()
					.Where (c 
					        => c.GetParameters ()
					        .Select (p => p.ParameterType)
					        .SequenceEqual (cacheArgs.Select (arg => arg != null ? arg.GetType () : typeof(List<double>))))
					.First ();

				var calculator = (TInquirer)ctor.Invoke (cacheArgs.ToArray ());
				data.Sort ();

				return calculator;
			} catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
					
		public abstract double CalcNthXile (IList<double> data, int xile, int nTh);
		
		public abstract double SampleMean (List<double> data);
	}
}
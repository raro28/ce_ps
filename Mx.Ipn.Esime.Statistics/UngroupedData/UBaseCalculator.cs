namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public abstract class UBaseCalculator
	{
		public ReadOnlyCollection<double> Data {
			get;
			private set;
		}
		
		public UBaseCalculator (IList<double> data)
		{
			AssertValidData (data);
			
			var cache = data.ToList ();
			cache.Sort ();

			Data = cache.AsReadOnly ();

			Init ();
		}

		protected static void AssertValidData (IList<double> rawData)
		{
			if (rawData == null) {
				throw new StatisticsException ("Null data set.", new ArgumentNullException ("data"));
			}
			
			if (rawData.Count == 0) {
				throw new StatisticsException ("Empty data set.");
			}
			
			if (rawData.Count == 1) {
				throw new StatisticsException ("Insufficient data.");
			}
		}

		protected abstract void InitCalculator ();

		private void Init ()
		{
			InitCalculator ();
		}
	}
}
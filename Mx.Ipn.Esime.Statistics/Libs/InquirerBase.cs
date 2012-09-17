namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public abstract class InquirerBase
	{
		public ReadOnlyCollection<double> Data {
			get;
			private set;
		}

		protected dynamic Inquirer {
			get;
			set;
		}

		public InquirerBase (IList<double> rawData)
		{
			AssertValidData (rawData);
			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();
			Data = readOnly;
		}

		protected static void AssertValidData (ICollection<double> data)
		{
			if (data == null) {
				throw new StatisticsException ("Null data set.", new ArgumentNullException ("data"));
			}
			
			if (data.Count == 0) {
				throw new StatisticsException ("Empty data set.");
			}
			
			if (data.Count == 1) {
				throw new StatisticsException ("Insufficient data.");
			}
		}
	}
}
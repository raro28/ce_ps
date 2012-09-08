namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class URangesCalculator:IRangesCalculator
	{
		public ReadOnlyCollection<double> Data {
			get;
			set;
		}

		private double? range;
		private double? qRange;
		private double? dRange;
		private double? pRange;

		public URangesCalculator (List<double> rawData)
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

			var cache = rawData.ToList ();
			cache.Sort ();

			Data = cache.AsReadOnly ();
		}

		public double GetDataRange ()
		{
			if (range == null) {
				range = Data.Max () - Data.Min ();
			}

			return (double)range;
		}

		public double GetInterquartileRange ()
		{
			if (qRange == null) {
				qRange = GetXile (XileOptions.Quartile, 3) - GetXile (XileOptions.Quartile, 1);
			}
			
			return (double)qRange;
		}

		public double GetInterdecileRange ()
		{
			if (dRange == null) {
				dRange = GetXile (XileOptions.Decile, 9) - GetXile (XileOptions.Decile, 1);
			}
			
			return (double)dRange;
		}

		public double GetInterpercentileRange ()
		{
			if (pRange == null) {
				pRange = GetXile (XileOptions.Percentile, 90) - GetXile (XileOptions.Percentile, 10);
			}
			
			return (double)pRange;
		}

		private double GetXile (XileOptions option, int number)
		{

			var lx = Data.Count * number / (double)option;
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);

			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;

			var xRange = iPortion * Data [li] + sPortion * Data [ls];

			return xRange;
		}

		private enum XileOptions
		{
			Quartile=4,
			Decile=10,
			Percentile=100
		}
	}
}
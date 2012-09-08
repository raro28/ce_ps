namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	
	public class UCentralTendecyCalculator:ICentralTendencyCalculator
	{

		public ReadOnlyCollection<double> Data {
			get;
			set;
		}

		private double? mean;
		private double? median;
		private List<double> mode;

		public UCentralTendecyCalculator (ReadOnlyCollection<double> sortedData)
		{
			AsserValidDataSet (sortedData);

			Data = sortedData;
		}

		public UCentralTendecyCalculator (List<double> rawData)
		{
			AsserValidDataSet (rawData);

			var cache = rawData.ToList ();
			cache.Sort ();
			Data = cache.AsReadOnly ();

			mode = new List<double> ();
		}

		public double GetMean ()
		{
			if (mean == null) {
				mean = Data.Sum () / Data.Count;
			}
			
			return (double)mean;
		}

		public double GetMedian ()
		{
			if (median == null) {
				var midIndex = (Data.Count / 2) - 1;
				median = Data.Count % 2 != 0 ? Data [midIndex + 1] : (Data [midIndex] + Data [midIndex + 1]) / 2;
			}
			
			return (double)median;
		}

		public IList<double> GetMode ()
		{
			if (mode.Count == 0) {
				var groups = Data.GroupBy (data => data);
				var modes = from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
					select _mode.First ();

				mode = modes.ToList ();
			}
			
			return mode;
		}

		static void AsserValidDataSet (IList<double> data)
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
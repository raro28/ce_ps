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

		public UCentralTendecyCalculator (List<double> data)
		{
			if (data == null) {
				
				throw new StatisticsException ("Null data set.", new ArgumentNullException ("data"));
			}
			
			if (data.Count == 0) {
				throw new StatisticsException ("Empty data set.");
			}
			
			if (data.Count == 1) {
				throw new StatisticsException ("Insuficient data.");
			}
			
			var cache = data.ToList ();
			cache.Sort ();
			Data = cache.AsReadOnly ();

			mode = new List<double> ();
		}

		private double? mean;

		private double? median;

		private List<double> mode;

		public double CalcMean ()
		{
			if (mean == null) {
				mean = Data.Sum () / Data.Count;
			}
			
			return (double)mean;
		}

		public double CalcMedian ()
		{
			if (median == null) {
				var midIndex = (Data.Count / 2) - 1;
				median = Data.Count % 2 != 0 ? Data [midIndex + 1] : (Data [midIndex] + Data [midIndex + 1]) / 2;
			}
			
			return (double)median;
		}

		public IList<double> CalcMode ()
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
	}
}

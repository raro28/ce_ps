namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	
	public class UCentralTendecyCalculator:UBaseCalculator,ICentralTendencyCalculator
	{
		private double? mean;
		private double? median;
		private List<double> mode;

		public UCentralTendecyCalculator (ReadOnlyCollection<double> sortedData):base(sortedData)
		{
		}

		public UCentralTendecyCalculator (List<double> rawData):base(rawData)
		{
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

		protected override void InitCalculator ()
		{
			mode = new List<double> ();
		}
	}
}
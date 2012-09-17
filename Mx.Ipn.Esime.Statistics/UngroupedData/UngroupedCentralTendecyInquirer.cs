namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	
	public class UngroupedCentralTendecyInquirer:InquirerBase,ICentralTendencyInquirer
	{
		private double? mean;
		private double? median;
		private List<double> mode;

		public UngroupedCentralTendecyInquirer (IList<double> rawData):base(rawData)
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
			if (mode == null) {
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
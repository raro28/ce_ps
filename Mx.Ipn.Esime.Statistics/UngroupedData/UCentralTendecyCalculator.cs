namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;
	
	public class UCentralTendecyCalculator:InquirerBase,ICentralTendencyCalculator
	{
		private double? mean;
		private double? median;
		private List<double> mode;

		public UCentralTendecyCalculator (IList<double> rawData):base(rawData)
		{	
			mode = new List<double> ();
		}

		public UCentralTendecyCalculator (ReadOnlyCollection<double> sortedData, IInquirer inquirer):base(sortedData, inquirer)
		{
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
	}
}
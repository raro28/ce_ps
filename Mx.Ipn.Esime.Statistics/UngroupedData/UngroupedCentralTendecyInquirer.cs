namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
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

		public UngroupedCentralTendecyInquirer (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetMean ()
		{
			if (mean == null) {

				mean = ((IEnumerable<double>)Inquirer.Data).Sum () / Inquirer.Data.Count;
			}
			
			return (double)mean;
		}

		public double GetMedian ()
		{
			if (median == null) {
				var midIndex = (Inquirer.Data.Count / 2) - 1;
				median = Inquirer.Data.Count % 2 != 0 ? Inquirer.Data [midIndex + 1] : (Inquirer.Data [midIndex] + Inquirer.Data [midIndex + 1]) / 2;
			}
			
			return (double)median;
		}

		public IList<double> GetMode ()
		{
			if (mode == null) {
				var groups = ((IEnumerable<double>)Inquirer.Data).GroupBy (data => data);
				var modes = from _mode in groups
					where _mode.Count () == groups.Max (grouped => grouped.Count ())
					select _mode.First ();

				mode = modes.ToList ();
			}
			
			return mode;
		}
	}
}
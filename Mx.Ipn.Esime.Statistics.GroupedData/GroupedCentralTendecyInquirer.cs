namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public GroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer (this);
			var xiles = new GroupedXileInquirer (distribution);

			Inquirer = new GroupedRangesInquirer (xiles);
		}

		public GroupedCentralTendecyInquirer (InquirerBase inquirer):base(inquirer)
		{			
		}

		protected override double CalcMean ()
		{
			Inquirer.AddFrequenciesTimesClassMarks ();
			var table = Inquirer.GetTable ();
			double fxSum = 0;
			foreach (var item in table) {
				fxSum += item.fX;
			}

			var mean = fxSum / Inquirer.Data.Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var median = Inquirer.GetQuartile (2);

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			Inquirer.AddFrequencies ();
			Inquirer.AddRealClassIntervals ();
			//FIXME cast of dynamic object to IEnumerable<double>
			var table = ((IEnumerable<dynamic>)Inquirer.GetTable ()).ToList ();
			var firstMaxFreqItem = table.OrderByDescending (item => item.Frequency).First ();
			var maxFreqItems = table.Where (item => item.Frequency == firstMaxFreqItem.Frequency).ToList ();

			var modes = new List<double> ();

			foreach (var item in maxFreqItems) {
				var maxFreqItem = maxFreqItems.First ();
				
				var iMaxFreqItem = table.IndexOf (maxFreqItem);
				
				var d1 = maxFreqItem.Frequency - (iMaxFreqItem != 0 ? table [iMaxFreqItem - 1].Frequency : 0);
				var d2 = maxFreqItem.Frequency - (iMaxFreqItem < (table.Count - 1) ? table [iMaxFreqItem + 1].Frequency : 0);
				
				var mode = maxFreqItem.RealInterval.From + ((d1 * Inquirer.Amplitude) / (d1 + d2));

				modes.Add (mode);
			}

			return modes;
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public GroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{			
			Inquirer = new DataDistributionFrequencyInquirer (this);
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
			List<dynamic> table = Enumerable.ToList (Inquirer.GetTable ());
			var firstMaxFreqItem = table.OrderByDescending (item => item.Frequency).First ();
			var maxFreqItems = table.Where (item => item.Frequency == firstMaxFreqItem.Frequency).ToList ();

			var modes = new List<double> ();

			foreach (var maxFreqItem in maxFreqItems) {				
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
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public GroupedCentralTendecyInquirer (IEnumerable<double> rawData):base(rawData)
		{			
			DistributionInquirer = new DataDistributionFrequencyInquirer (rawData);
			XileInquirer = new GroupedXileInquirer (rawData);
		}

		private DataDistributionFrequencyInquirer DistributionInquirer {
			get;
			set;
		}

		private GroupedXileInquirer XileInquirer {
			get;
			set;
		}

		protected override double CalcMean ()
		{
			DistributionInquirer.AddFrequenciesTimesClassMarks ();
			var table = DistributionInquirer.GetTable ();
			double fxSum = 0;
			foreach (var item in table) {
				fxSum += item.fX;
			}

			var mean = fxSum / Data.Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var median = XileInquirer.GetQuartile (2);

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			DistributionInquirer.AddFrequencies ();
			DistributionInquirer.AddRealClassIntervals ();
			var table = DistributionInquirer.GetTable ().ToList ();
			var firstMaxFreqItem = table.OrderByDescending (item => item.Frequency).First ();
			var maxFreqItems = table.Where (item => item.Frequency == firstMaxFreqItem.Frequency).ToList ();

			var modes = new List<double> ();

			foreach (var maxFreqItem in maxFreqItems) {				
				var iMaxFreqItem = table.IndexOf (maxFreqItem);
				
				var d1 = maxFreqItem.Frequency - (iMaxFreqItem != 0 ? table [iMaxFreqItem - 1].Frequency : 0);
				var d2 = maxFreqItem.Frequency - (iMaxFreqItem < (table.Count - 1) ? table [iMaxFreqItem + 1].Frequency : 0);
				
				var mode = maxFreqItem.RealInterval.From + ((d1 * DistributionInquirer.Amplitude) / (d1 + d2));

				modes.Add (mode);
			}

			return modes;
		}
	}
}
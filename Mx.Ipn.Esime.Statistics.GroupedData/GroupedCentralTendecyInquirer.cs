namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedCentralTendecyInquirer:CentralTendecyInquirerBase
	{
		public GroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer (this);
			Properties ["Inquirers"].Add (distribution);
			Properties ["Inquirers"].Add (new GroupedXileInquirer (this));
		}

		protected override double CalcMean ()
		{
			DynamicSelf.AddFrequenciesTimesClassMarks ();
			var table = DynamicSelf.GetTable ();
			double fxSum = 0;
			foreach (var item in table) {
				fxSum += item.fX;
			}

			var mean = fxSum / Properties ["Data"].Count;

			return mean;
		}

		protected override double CalcMedian ()
		{
			var median = DynamicSelf.GetQuartile (2);

			return median;
		}

		protected override IList<double> CalcModes ()
		{
			DynamicSelf.AddFrequencies ();
			DynamicSelf.AddRealClassIntervals ();
			List<dynamic> table = Enumerable.ToList (DynamicSelf.GetTable ());
			var firstMaxFreqItem = table.OrderByDescending (item => item.Frequency).First ();
			var maxFreqItems = table.Where (item => item.Frequency == firstMaxFreqItem.Frequency).ToList ();

			var modes = new List<double> ();

			foreach (var maxFreqItem in maxFreqItems) {				
				var iMaxFreqItem = table.IndexOf (maxFreqItem);
				
				var d1 = maxFreqItem.Frequency - (iMaxFreqItem != 0 ? table [iMaxFreqItem - 1].Frequency : 0);
				var d2 = maxFreqItem.Frequency - (iMaxFreqItem < (table.Count - 1) ? table [iMaxFreqItem + 1].Frequency : 0);
				
				var mode = maxFreqItem.RealInterval.From + ((d1 * Properties ["Amplitude"]) / (d1 + d2));

				modes.Add (mode);
			}

			return modes;
		}
	}
}
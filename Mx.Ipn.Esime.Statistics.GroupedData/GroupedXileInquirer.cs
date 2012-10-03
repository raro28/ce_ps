namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedXileInquirer:XileInquirerBase
	{
		public GroupedXileInquirer (IEnumerable<double> rawData):base(rawData)
		{			
			DistributionInquirer = new DataDistributionFrequencyInquirer (rawData);
		}

		private DataDistributionFrequencyInquirer DistributionInquirer {
			get;
			set;
		}

		protected override double CalcXile (double lx)
		{
			DistributionInquirer.AddFrequencies ();
			DistributionInquirer.AddAcumulatedFrequencies ();
			DistributionInquirer.AddRealClassIntervals ();
			var table = DistributionInquirer.GetTable ().Skip (1);
			dynamic prevElement = null;
			dynamic targetElement = null;
			foreach (var item in table) {
				targetElement = item;
				if (targetElement.AcumulatedFrequency >= lx) {
					break;
				}
				
				prevElement = item;
			}
			
			double prevF = prevElement != null ? prevElement.AcumulatedFrequency : 0;
			var xileResult = targetElement.RealInterval.From + ((lx - prevF) / targetElement.Frequency) * DistributionInquirer.Amplitude;
			
			return xileResult;
		}
	}
}
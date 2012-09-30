namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedXileInquirer:XileInquirerBase
	{
		public GroupedXileInquirer (List<double> rawData):base(rawData)
		{			
			Properties ["Inquirers"].Add (new DataDistributionFrequencyInquirer (this));
		}

		public GroupedXileInquirer (InquirerBase inquirer):base(inquirer)
		{			
		}

		protected override double CalcXile (double lx)
		{
			DynamicSelf.AddFrequencies ();
			DynamicSelf.AddAcumulatedFrequencies ();
			DynamicSelf.AddRealClassIntervals ();
			List<dynamic> table = Enumerable.ToList (DynamicSelf.GetTable ());
			dynamic prevElement = null;
			dynamic targetElement = null;
			foreach (var item in table.Skip (1)) {
				targetElement = item;
				if (targetElement.AcumulatedFrequency >= lx) {
					break;
				}
				
				prevElement = item;
			}
			
			double prevF = prevElement != null ? prevElement.AcumulatedFrequency : 0;
			var xileResult = targetElement.RealInterval.From + ((lx - prevF) / targetElement.Frequency) * Properties ["Amplitude"];
			
			return xileResult;
		}
	}
}
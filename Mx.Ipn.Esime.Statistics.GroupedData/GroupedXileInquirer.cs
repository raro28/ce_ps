namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedXileInquirer:XileInquirerBase
	{
		public GroupedXileInquirer (InquirerBase inquirer):base(inquirer)
		{			
		}

		public GroupedXileInquirer (List<double> rawData):base(rawData)
		{			
			Inquirer = new DataDistributionFrequencyInquirer (this);
		}

		protected override double CalcXile (double lx)
		{
			Inquirer.AddFrequencies ();
			Inquirer.AddAcumulatedFrequencies ();
			Inquirer.AddRealClassIntervals ();
			List<dynamic> table = Enumerable.ToList (Inquirer.GetTable ());
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
			var xileResult = targetElement.RealInterval.From + ((lx - prevF) / targetElement.Frequency) * Inquirer.Amplitude;
			
			return xileResult;
		}
	}
}
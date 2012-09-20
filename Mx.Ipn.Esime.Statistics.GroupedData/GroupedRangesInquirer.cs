namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class GroupedRangesInquirer:RangesInquirerBase
	{
		public GroupedRangesInquirer (List<double> rawData):base(rawData)
		{	
			var distribution = new DataDistributionFrequencyInquirer (this);
			Inquirer = new GroupedXileInquirer (distribution);
		}

		public GroupedRangesInquirer (InquirerBase inquirer):base(inquirer)
		{		
		}

		protected override double CalcDataRange ()
		{
			Inquirer.AddClassIntervals ();
			List<dynamic> table = Enumerable.ToList (Inquirer.GetTable ());
			var range = table.Last ().ClassInterval.To - table.First ().ClassInterval.From;

			return range;
		}
	}
}
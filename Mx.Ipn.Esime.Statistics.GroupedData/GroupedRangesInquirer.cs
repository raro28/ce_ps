namespace Mx.Ipn.Esime.Statistics.GroupedData
{
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
			var table = Inquirer.GetTable ();
			var range = table [0].ClassInterval.To - table [table.Count - 1].ClassInterval.From;

			return range;
		}
	}
}
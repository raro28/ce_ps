namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

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

		protected override double CalcRange ()
		{
			var table = ((IEnumerable<Interval>)Inquirer.GetClassIntervalsTable ()).ToList ();
			var range = table.Last ().To - table.First ().From;

			return range;
		}
	}
}
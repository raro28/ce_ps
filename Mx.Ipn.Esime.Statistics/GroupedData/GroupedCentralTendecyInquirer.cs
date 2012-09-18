namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

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
			var fxSum = Enumerable.Sum (Inquirer.GetFrequenciesTimesClassMarksTable ());
			var mean = fxSum / Inquirer.Data.Count;

			return mean;
		}

		protected override double CalMedian ()
		{
			throw new System.NotImplementedException ();
		}

		protected override IList<double> CalModes ()
		{
			throw new System.NotImplementedException ();
		}
	}
}
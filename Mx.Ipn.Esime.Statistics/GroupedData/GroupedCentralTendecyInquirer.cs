namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedCentralTendecyInquirer:InquirerBase, ICentralTendencyInquirer
	{
		public GroupedCentralTendecyInquirer (List<double> rawData):base(rawData)
		{			
			var distribution = new DataDistributionFrequencyInquirer(this);
			var xiles = new GroupedXileInquirer(distribution);

			Inquirer = new GroupedRangesInquirer(xiles);
		}

		public GroupedCentralTendecyInquirer (InquirerBase inquirer):base(inquirer)
		{			
		}

		public double GetMean ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mean)")) {
				var fxSum = Enumerable.Sum (Inquirer.GetFrequenciesTimesClassMarksTable ());
				var mean = fxSum / Inquirer.Data.Count;

				Inquirer.Answers.Add ("get(mean)", mean);
			}
			
			return Inquirer.Answers ["get(mean)"];
		}

		public double GetMedian ()
		{
			throw new System.NotImplementedException ();
		}

		public IList<double> GetMode ()
		{
			throw new System.NotImplementedException ();
		}
	}
}
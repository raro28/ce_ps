namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedRangesInquirer:InquirerBase,IRangesInquirer
	{
		public GroupedRangesInquirer (List<double> rawData):base(rawData)
		{	
			var distribution = new DataDistributionFrequencyInquirer(this);
			Inquirer = new GroupedXileInquirer(distribution);
		}

		public GroupedRangesInquirer (InquirerBase inquirer):base(inquirer)
		{		
		}

		public double GetDataRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterquartileRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterdecileRange ()
		{
			throw new System.NotImplementedException ();
		}

		public double GetInterpercentileRange ()
		{
			throw new System.NotImplementedException ();
		}
	}
}
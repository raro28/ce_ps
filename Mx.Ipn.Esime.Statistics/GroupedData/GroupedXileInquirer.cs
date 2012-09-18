namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedXileInquirer:InquirerBase,IXileInquirer
	{
		public GroupedXileInquirer (InquirerBase inquirer):base(inquirer)
		{			
		}

		public GroupedXileInquirer (List<double> rawData):base(rawData)
		{			
			Inquirer = new DataDistributionFrequencyInquirer(this);
		}

		public double GetDecile (int nTh)
		{
			throw new System.NotImplementedException ();
		}

		public double GetPercentile (int nTh)
		{
			throw new System.NotImplementedException ();
		}

		public double GetQuartile (int nTh)
		{
			throw new System.NotImplementedException ();
		}
	}
}
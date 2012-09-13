namespace Mx.Ipn.Esime.Statistics.Grouped
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedXileInquirer:InquirerBase,IXileInquirer
	{
		public GroupedXileInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public GroupedXileInquirer (ReadOnlyCollection<double> sortedData, IDistributionChartInquirer inquirer):base(sortedData, inquirer)
		{
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
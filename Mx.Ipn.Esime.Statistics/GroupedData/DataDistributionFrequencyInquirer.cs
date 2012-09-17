namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class DataDistributionFrequencyInquirer:InquirerBase,IDistributionChartInquirer
	{
		public DataDistributionFrequencyInquirer (IList<double> rawData):base(rawData)
		{			
		}
	}	
}
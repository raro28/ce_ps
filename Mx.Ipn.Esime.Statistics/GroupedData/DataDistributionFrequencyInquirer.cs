namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class DataDistributionFrequencyInquirer:InquirerBase,IDistributionChartInquirer
	{
		public DataDistributionFrequencyInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public DataDistributionFrequencyInquirer (ReadOnlyCollection<double> sortedData, IInquirer inquirer):base(sortedData,inquirer)
		{
		}	
	}	
}
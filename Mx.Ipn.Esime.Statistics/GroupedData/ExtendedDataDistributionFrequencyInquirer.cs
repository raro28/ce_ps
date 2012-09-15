namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class ExtendedDataDistributionFrequencyInquirer:DataDistributionFrequencyInquirer, IExtendedDistributionChartInquirer
	{
		public ExtendedDataDistributionFrequencyInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public ExtendedDataDistributionFrequencyInquirer (ReadOnlyCollection<double> sortedData, GroupedStatisticsInquirer inquirer):base(sortedData,inquirer)
		{
		}

		public IEnumerable<double> GetMeanDifference (int nthDifference)
		{
			throw new System.NotImplementedException ();
		}
	}
}
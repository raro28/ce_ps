namespace Mx.Ipn.Esime.Statistics.GroupedData
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class ExtendedDataDistributionFrequencyInquirer:DataDistributionFrequencyInquirer, IExtendedDistributionChartInquirer
	{
		public ExtendedDataDistributionFrequencyInquirer (List<double> rawData):base(rawData)
		{			
		}

		public IEnumerable<double> GetMeanDifference (int nthDifference)
		{
			throw new System.NotImplementedException ();
		}
	}
}
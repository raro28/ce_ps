namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public interface IExtendedDistributionChartInquirer:IDistributionChartInquirer
	{
		IEnumerable<double> GetMeanDifference (int nthDifference);
	}
}

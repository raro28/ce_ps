namespace Mx.Ipn.Esime.Statistics.Core
{
	public interface IXileInquirer
	{
		double GetDecile (int nTh);

		double GetPercentile (int nTh);

		double GetQuartile (int nTh);
	}
}
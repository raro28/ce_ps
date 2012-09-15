namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IXileInquirer
	{
		double GetDecile (int nTh);

		double GetPercentile (int nTh);

		double GetQuartile (int nTh);
	}
}
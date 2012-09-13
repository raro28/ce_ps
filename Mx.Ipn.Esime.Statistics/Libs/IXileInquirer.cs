namespace Mx.Ipn.Esime.Statistics.Libs
{
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public interface IXileInquirer:IInquirer
	{
		double GetDecile (int nTh);

		double GetPercentile (int nTh);

		double GetQuartile (int nTh);
	}
}
namespace Mx.Ipn.Esime.Statistics.Core
{
	public interface IRangesInquirer
	{
		double GetDataRange ();
		
		double GetInterQuartileRange ();
		
		double GetInterDecileRange ();
		
		double GetInterPercentileRange ();
	}
}
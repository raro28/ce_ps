namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IRangesInquirer
	{
		double GetDataRange ();
		
		double GetInterQuartileRange ();
		
		double GetInterDecileRange ();
		
		double GetInterPercentileRange ();
	}
}
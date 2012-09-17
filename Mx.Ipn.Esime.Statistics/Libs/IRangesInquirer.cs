namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IRangesInquirer
	{
		double GetDataRange ();
		
		double GetInterquartileRange ();
		
		double GetInterdecileRange ();
		
		double GetInterpercentileRange ();
	}
}
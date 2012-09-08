namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IRangesCalculator
	{
		double GetDataRange ();
		
		double GetInterquartileRange ();
		
		double GetInterdecileRange ();
		
		double GetInterpercentileRange ();
	}
	
}
namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IRangesCalculator
	{
		double CalcDataRange ();
		
		double CalcInterquartileRange ();
		
		double CalcInterdecileRange ();
		
		double CalcInterpercentileRange ();
	}
	
}

namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IRangesInquirer:IInquirer
	{
		double GetDataRange ();
		
		double GetInterquartileRange ();
		
		double GetInterdecileRange ();
		
		double GetInterpercentileRange ();
	}
}
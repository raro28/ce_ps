namespace Mx.Ipn.Esime.Statistics.Libs
{
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public interface IRangesCalculator:IInquirer
	{
		double GetDataRange ();
		
		double GetInterquartileRange ();
		
		double GetInterdecileRange ();
		
		double GetInterpercentileRange ();
	}
}
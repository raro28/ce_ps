namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public interface ICentralTendencyInquirer
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
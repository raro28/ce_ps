namespace Mx.Ipn.Esime.Statistics.Core
{
	using System.Collections.Generic;

	public interface ICentralTendencyInquirer
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
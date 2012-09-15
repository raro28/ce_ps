namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public interface ICentralTendencyInquirer:IInquirer
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
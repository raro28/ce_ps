namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public interface ICentralTendencyCalculator
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
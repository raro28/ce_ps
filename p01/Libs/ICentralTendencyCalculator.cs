namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;

	public interface ICentralTendencyCalculator
	{
		double CalcMean ();
		
		double CalcMedian ();
		
		IList<double> CalcMode ();
	}
	
}

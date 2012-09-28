namespace Mx.Ipn.Esime.Statistics.Core
{
	public interface IDispersionInquirer
	{
		double GetDataRange ();
		
		double GetInterQuartileRange ();
		
		double GetInterDecileRange ();
		
		double GetInterPercentileRange ();

		double GetAbsoluteDeviation (double mean);
		
		double GetVariance (double mean);
		
		double GetStandarDeviation (double mean);
		
		double GetCoefficientOfVariation (double mean);
		
		double GetCoefficientOfSymmetry (double mean);
		
		double GetCoefficientOfKourtosis (double mean);
	}	
}
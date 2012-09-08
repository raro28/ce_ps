namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IDispersionCalculator
	{
		double GetAbsoluteDeviation ();
		
		double GetVariance ();
		
		double GetStandarDeviation ();
		
		double GetCoefficientOfVariation ();
		
		double GetCoefficientOfSymmetry ();
		
		double GetCoefficientOfKourtosis ();
	}	
}
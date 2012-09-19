namespace Mx.Ipn.Esime.Statistics.Core
{
	public interface IDispersionInquirer
	{
		double GetAbsoluteDeviation ();
		
		double GetVariance ();
		
		double GetStandarDeviation ();
		
		double GetCoefficientOfVariation ();
		
		double GetCoefficientOfSymmetry ();
		
		double GetCoefficientOfKourtosis ();
	}	
}
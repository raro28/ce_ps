namespace Mx.Ipn.Esime.Statistics.Libs
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
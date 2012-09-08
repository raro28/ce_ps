namespace Mx.Ipn.Esime.Statistics.Libs
{
	public interface IDispersionCalculator
	{
		double CalcAbsoluteDeviation ();
		
		double CalcVariance ();
		
		double CalcStandarDeviation ();
		
		double CalcCoefficientOfVariation ();
		
		double CalcCoefficientOfSymetry ();
		
		double CalcCoefficientOfKourtosis ();
	}
	
}

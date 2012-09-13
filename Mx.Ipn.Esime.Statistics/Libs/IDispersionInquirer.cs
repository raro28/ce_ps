namespace Mx.Ipn.Esime.Statistics.Libs
{
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public interface IDispersionInquirer:IInquirer
	{
		double GetAbsoluteDeviation ();
		
		double GetVariance ();
		
		double GetStandarDeviation ();
		
		double GetCoefficientOfVariation ();
		
		double GetCoefficientOfSymmetry ();
		
		double GetCoefficientOfKourtosis ();
	}	
}
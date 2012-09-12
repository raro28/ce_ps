
namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public interface ICentralTendencyCalculator:IInquirer
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
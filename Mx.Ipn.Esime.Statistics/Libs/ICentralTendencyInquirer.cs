
namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public interface ICentralTendencyInquirer:IInquirer
	{
		double GetMean ();
		
		double GetMedian ();
		
		IList<double> GetMode ();
	}	
}
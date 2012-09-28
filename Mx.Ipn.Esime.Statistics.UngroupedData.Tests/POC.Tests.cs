namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class POC_Tests:InquirerBase_Tests<UngroupedDispersionInquirer,UngroupedHelperMethods>
	{
		[Test()]
		public void Inquirer_POC_Test ()
		{
			List<double> data = Helper.GetRandomDataSample (25).ToList ();
			dynamic calculator = Helper.NewInquirer<UngroupedDispersionInquirer> (data);

			calculator.GetMean ();
			calculator.GetModes ();
			calculator.GetMedian ();
			calculator.GetQuartile (3);
			calculator.GetDecile (6);
			calculator.GetPercentile (49);
			calculator.GetDataRange ();
			calculator.GetAbsoluteDeviation ();
			calculator.GetVariance ();
			calculator.GetStandarDeviation ();
			calculator.GetCoefficientOfVariation ();
			calculator.GetCoefficientOfSymmetry ();
			calculator.GetCoefficientOfKourtosis ();
		}
	}
}
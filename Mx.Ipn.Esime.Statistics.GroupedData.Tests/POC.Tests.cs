namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class POC_Tests:InquirerBase_Tests<GroupedDispersionInquirer,GroupedHelperMethods>
	{
		[Test()]
		public void Inquirer_POC_Test ()
		{
			List<double> data = Helper.GetRandomDataSample (25).ToList ();
			dynamic calculator = Helper.NewInquirer<GroupedDispersionInquirer> (data);

			calculator.GetMean ();
			calculator.GetTable ();
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
			calculator.AddMeanDifference (2);
			calculator.AddMeanDifference (3);
			calculator.AddMeanDifference (4);
			calculator.AddClassIntervals ();
			calculator.AddFrequencies ();
			calculator.AddAcumulatedFrequencies ();
			calculator.AddRelativeFrequencies ();
			calculator.AddAcumulatedRelativeFrequencies ();
			calculator.AddClassMarks ();
			calculator.AddRealClassIntervals ();
			calculator.AddFrequenciesTimesClassMarks ();
		}
	}
}
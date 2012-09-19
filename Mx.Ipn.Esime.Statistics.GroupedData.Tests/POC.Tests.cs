namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class POC_Tests:InquirerBase_Tests<GroupedDispersionInquirer,GroupedHelperMethods<GroupedDispersionInquirer>>
	{
		public POC_Tests ():base(()=>{return new GroupedDispersionInquirer (rawData: null);})
		{
		}

		[Test()]
		public void Inquirer_POC_Test ()
		{
			List<double> sortedData;
			dynamic calculator = Helper.NewInquirer (out sortedData, size: 25);

			calculator.GetMean ();
			calculator.GetTable ();
			calculator.GetMode ();
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
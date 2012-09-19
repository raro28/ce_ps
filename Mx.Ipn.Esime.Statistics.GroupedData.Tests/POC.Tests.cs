namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class POC_Tests:InquirerBase_Tests<GroupedDispersionInquirer>
	{
		public POC_Tests ():base(()=>{return new GroupedDispersionInquirer (rawData: null);}, new GroupedHelperMethods<GroupedDispersionInquirer> ())
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
			((IEnumerable<double>)calculator.GetMeanDifference (2)).ToList ();
			((IEnumerable<double>)calculator.GetMeanDifference (3)).ToList ();
			((IEnumerable<double>)calculator.GetMeanDifference (4)).ToList ();
			((IEnumerable<Interval>)calculator.GetClassIntervals ()).ToList ();
			((IEnumerable<int>)calculator.GetFrequencies ()).ToList ();
			((IEnumerable<int>)calculator.GetAcumulatedFrequencies ()).ToList ();
			((IEnumerable<double>)calculator.GetRelativeFrequencies ()).ToList ();
			((IEnumerable<double>)calculator.GetAcumulatedRelativeFrequencies ()).ToList ();
			((IEnumerable<double>)calculator.GetClassMarks ()).ToList ();
			((IEnumerable<Interval>)calculator.GetRealClassIntervals ()).ToList ();
			((IEnumerable<double>)calculator.GetFrequenciesTimesClassMarks ()).ToList ();
		}
	}
}
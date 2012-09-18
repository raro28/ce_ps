namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;

	[TestFixture()]
	public class DataDistributionFrequencyInquirer_Tests
	{
		[Test()]
		public void Inquirer_POC_Test ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedDispersionInquirer> (out sortedData, size: 100);

			calculator.GetMean ();
			calculator.GetMode ();
			calculator.GetMedian ();
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
			((IEnumerable<Interval>)calculator.GetClassIntervalsTable ()).ToList ();
			((IEnumerable<int>)calculator.GetFrequencyTable ()).ToList ();
			((IEnumerable<int>)calculator.GetAcumulatedFrequencyTable ()).ToList ();
			((IEnumerable<double>)calculator.GetRelativeFrequencyTable ()).ToList ();
			((IEnumerable<double>)calculator.GetAcumulatedRelativeFrequencyTable ()).ToList ();
			((IEnumerable<double>)calculator.GetClassMarksTable ()).ToList ();
			((IEnumerable<Interval>)calculator.GetRealClassIntervalsTable ()).ToList ();
			((IEnumerable<double>)calculator.GetFrequenciesTimesClassMarksTable ()).ToList ();
		}
	}
}
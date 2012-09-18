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
			DataDistributionFrequencyInquirer calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 7);
			calculator.GetClassIntervalsTable ().ToList ();
			calculator.GetFrequencyTable ().ToList ();
			calculator.GetAcumulatedFrequencyTable ().ToList ();
			calculator.GetRelativeFrequencyTable ().ToList ();
			calculator.GetAcumulatedRelativeFrequencyTable ().ToList ();
			calculator.GetClassMarksTable ().ToList ();
			calculator.GetRealClassIntervalsTable ().ToList ();
		}
	}
}
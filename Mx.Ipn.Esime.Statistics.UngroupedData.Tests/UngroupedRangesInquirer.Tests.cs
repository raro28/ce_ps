namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UngroupedRangesInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedRangesInquirer>
	{
		public UngroupedRangesInquirer_Tests ()
		{
			InitializeFaultInquirerWithNullDataSet = () => {
				return new UngroupedRangesInquirer (rawData: null);
			};
		}

		[Test()]
		public void Inquirer_Gets_Expected_Data_Range ()
		{
			List<double> sortedData;
			var calculator = Helper.NewInstance (out sortedData, size: 100);

			var expected = sortedData [sortedData.Count - 1] - sortedData [0];
			var actual = calculator.GetDataRange ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges ()
		{
			List<double> sortedData;
			var calculator = Helper.NewInstance (out sortedData, size: 7);

			var expected = Helper.CalcNthXile (sortedData, 4, 3) - Helper.CalcNthXile (sortedData, 4, 1);
			var actual = calculator.GetInterquartileRange ();
			Assert.AreEqual (expected, actual);
			expected = Helper.CalcNthXile (sortedData, 10, 9) - Helper.CalcNthXile (sortedData, 10, 1);
			actual = calculator.GetInterdecileRange ();
			Assert.AreEqual (expected, actual);
			expected = Helper.CalcNthXile (sortedData, 100, 90) - Helper.CalcNthXile (sortedData, 100, 10);
			actual = calculator.GetInterpercentileRange ();
			Assert.AreEqual (expected, actual);
		}
	}
}
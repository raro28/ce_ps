namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UngroupedRangesInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedRangesInquirer>
	{
		[Test()]
		public void Inquirer_Gets_Expected_Data_Range ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedRangesInquirer>.NewInstance (out sortedData, size: 100);

			Inquirer_Gets_Expected_Data_Range (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Data_Range (List<double> sortedData, dynamic calculator)
		{
			var expected = sortedData [sortedData.Count - 1] - sortedData [0];
			var actual = calculator.GetDataRange ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedRangesInquirer>.NewInstance (out sortedData, size: 7);

			Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges (List<double> sortedData, dynamic calculator)
		{
			var expected = HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 4, 3) - HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 4, 1);
			var actual = calculator.GetInterquartileRange ();
			Assert.AreEqual (expected, actual);
			expected = HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 10, 9) - HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 10, 1);
			actual = calculator.GetInterdecileRange ();
			Assert.AreEqual (expected, actual);
			expected = HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 100, 90) - HelperMethods<UngroupedRangesInquirer>.CalcNthXile (sortedData, 100, 10);
			actual = calculator.GetInterpercentileRange ();
			Assert.AreEqual (expected, actual);
		}

		protected override void InitializeFaultInquirerWithNullDataSet ()
		{
			new UngroupedCentralTendecyInquirer (rawData: null);
		}
	}
}
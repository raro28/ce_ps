namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;

	[TestFixture()]
	public class GroupedRangesInquirer_Tests
	{
		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedRangesInquirer> (out sortedData, size: 100);

			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void Inquirer_Gets_Expected_Data_Range ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedRangesInquirer> (out sortedData, size: 100);

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
			var calculator = HelperMethods.NewInstanceOf<GroupedRangesInquirer> (out sortedData, size: 7);

			Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges (List<double> sortedData, dynamic calculator)
		{
			var expected = HelperMethods.CalcNthXile (sortedData, 4, 3) - HelperMethods.CalcNthXile (sortedData, 4, 1);
			var actual = calculator.GetInterquartileRange ();
			Assert.AreEqual (expected, actual);
			expected = HelperMethods.CalcNthXile (sortedData, 10, 9) - HelperMethods.CalcNthXile (sortedData, 10, 1);
			actual = calculator.GetInterdecileRange ();
			Assert.AreEqual (expected, actual);
			expected = HelperMethods.CalcNthXile (sortedData, 100, 90) - HelperMethods.CalcNthXile (sortedData, 100, 10);
			actual = calculator.GetInterpercentileRange ();
			Assert.AreEqual (expected, actual);
		}
	}
}
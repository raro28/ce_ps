namespace Mx.Ipn.Esime.Statistics.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class URangesCalculator_Tests
	{
		[Test()]
		public void Calculator_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var rCalc = HelperMethods.NewInstanceOf<URangesCalculator> (out sortedData, size: 100);

			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], rCalc.Data [i]);
			}
		}

		[Test()]
		public void Calculator_Gets_Expected_Data_Range ()
		{
			List<double> sortedData;
			var rCalc = HelperMethods.NewInstanceOf<URangesCalculator> (out sortedData, size: 100);

			var expected = sortedData [sortedData.Count - 1] - sortedData [0];
			var actual = rCalc.GetDataRange ();

			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Calculator_Gets_Expected_Quartil_Decil_Percentil_Ranges ()
		{
			List<double> sortedData;
			var rCalc = HelperMethods.NewInstanceOf<URangesCalculator> (out sortedData, size: 7);

			var expected = CalcX (sortedData, 4, 3) - CalcX (sortedData, 4, 1);
			var actual = rCalc.GetInterquartileRange ();
			
			Assert.AreEqual (expected, actual);

			expected = CalcX (sortedData, 10, 9) - CalcX (sortedData, 10, 1);
			actual = rCalc.GetInterdecileRange ();
			
			Assert.AreEqual (expected, actual);

			expected = CalcX (sortedData, 100, 90) - CalcX (sortedData, 100, 10);
			actual = rCalc.GetInterpercentileRange ();
			
			Assert.AreEqual (expected, actual);
		}

		private double CalcX (IList<double> data, int range, int number)
		{
			var lx = data.Count * number / (double)range;
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);
			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;
			Console.WriteLine ("{0} {1} {2} {3}", lx, li, ls, iPortion, sPortion);
			var xRange = iPortion * data [li] + sPortion * data [ls];
			
			return xRange;
		}
	}
}
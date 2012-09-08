namespace Mx.Ipn.Esime.Statistics.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UCentralTendecyCalculator_Tests
	{
		[Test()]
		public void Calculator_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], rCalc.Data [i]);
			}
		}

		[Test()]
		public void Calculator_Gets_Expected_Mean ()
		{
			List<double> sortedData;
			var rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (out sortedData, size: 100);
			var sum = 0.0;
			sortedData.ForEach (data => sum += data);
			var expected = sum / sortedData.Count;
			var actual = rCalc.GetMean ();
			
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Calculator_Gets_Expected_Mode ()
		{
			List<double> sortedData = new List<double>{1,2,3,2};
			var rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (ref sortedData);
			var expected = new List<double>{2};
			var actual = rCalc.GetMode ();

			Assert.AreEqual (expected, actual);

			sortedData = new List<double>{1,2,3,2,4,7,8,7};
			rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (ref sortedData);
			expected = new List<double>{2,7};
			actual = rCalc.GetMode ();
			
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Calculator_Gets_Expected_Median ()
		{
			List<double> sortedData = new List<double>{1,2,3};
			var rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (ref sortedData);
			var expected = 2.0;
			var actual = rCalc.GetMedian ();
			
			Assert.AreEqual (expected, actual);
			
			sortedData = new List<double>{1,2,3,4,5,6,7,8};
			rCalc = HelperMethods.NewInstanceOf<UCentralTendecyCalculator> (ref sortedData);
			expected = 4.5;
			actual = rCalc.GetMedian ();
			
			Assert.AreEqual (expected, actual);
		}
	}
}
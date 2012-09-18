namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	
	[TestFixture()]
	public class UngroupedCentralTendecyInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedCentralTendecyInquirer>
	{	
		[Test()]
		public void Inquirer_Gets_Expected_Mean ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedCentralTendecyInquirer>.NewInstance (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Mean (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Mean (List<double> sortedData, dynamic calculator)
		{
			var sum = 0.0;
			sortedData.ForEach (data => sum += data);
			var expected = sum / sortedData.Count;
			var actual = calculator.GetMean ();
			Assert.AreEqual (expected, actual);
		}
		
		[Test()]
		public void Inquirer_Gets_Expected_Mode ()
		{
			List<double> sortedData = new List<double>{1,2,3,2};
			var calculator = HelperMethods<UngroupedCentralTendecyInquirer>.NewInstance (ref sortedData);
			
			Inquirer_Gets_Expected_Mode (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Mode (List<double> sortedData, dynamic calculator)
		{
			var expected = new List<double> {2};
			var actual = calculator.GetMode ();
			Assert.AreEqual (expected, actual);
			sortedData = new List<double> {1,2,3,2,4,7,8,7};
			calculator = HelperMethods<UngroupedCentralTendecyInquirer>.NewInstance (ref sortedData);
			expected = new List<double> {2,7};
			actual = calculator.GetMode ();
			Assert.AreEqual (expected, actual);
		}
		
		[Test()]
		public void Inquirer_Gets_Expected_Median ()
		{
			List<double> sortedData = new List<double>{1,2,3};
			var calculator = HelperMethods<UngroupedCentralTendecyInquirer>.NewInstance (ref sortedData);
			
			Inquirer_Gets_Expected_Median (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Median (List<double> sortedData, dynamic calculator)
		{
			var expected = 2.0;
			var actual = calculator.GetMedian ();
			Assert.AreEqual (expected, actual);
			sortedData = new List<double> {1,2,3,4,5,6,7,8};
			calculator = HelperMethods<UngroupedCentralTendecyInquirer>.NewInstance (ref sortedData);
			expected = 4.5;
			actual = calculator.GetMedian ();
			Assert.AreEqual (expected, actual);
		}

		protected override void InitializeFaultInquirerWithNullDataSet ()
		{
			new UngroupedCentralTendecyInquirer (rawData: null);
		}
	}
}
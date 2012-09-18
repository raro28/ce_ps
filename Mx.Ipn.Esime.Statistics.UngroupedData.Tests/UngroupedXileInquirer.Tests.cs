namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.Libs;

	[TestFixture()]
	public class UngroupedXileInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedXileInquirer>
	{
		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Quartiles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 7);
			try {
				calculator.GetQuartile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
			
			try {
				calculator.GetQuartile (5);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}
		
		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Deciles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 7);
			try {
				calculator.GetDecile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
			
			try {
				calculator.GetDecile (11);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}
		
		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Percentiles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 7);
			try {
				calculator.GetPercentile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
			
			try {
				calculator.GetPercentile (101);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartiles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Quartiles (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Quartiles (List<double> sortedData, dynamic calculator)
		{
			var expected = GetXiles (4, nTh => HelperMethods<UngroupedXileInquirer>.CalcNthXile (sortedData, 4, nTh)).ToList ();
			var actual = GetXiles (4, nTh => calculator.GetQuartile (nTh)).ToList ();
			CollectionAssert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Deciles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Deciles (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Deciles (List<double> sortedData, dynamic calculator)
		{
			var expected = GetXiles (10, nTh => HelperMethods<UngroupedXileInquirer>.CalcNthXile (sortedData, 10, nTh)).ToList ();
			var actual = GetXiles (10, nTh => calculator.GetDecile (nTh)).ToList ();
			CollectionAssert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Percentiles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 100);

			Inquirer_Gets_Expected_Percentiles (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Percentiles (List<double> sortedData, dynamic calculator)
		{
			var expected = GetXiles (100, nTh => HelperMethods<UngroupedXileInquirer>.CalcNthXile (sortedData, 100, nTh)).ToList ();
			var actual = GetXiles (100, nTh => calculator.GetPercentile (nTh)).ToList ();
			CollectionAssert.AreEqual (expected, actual);
		}

		protected override void InitializeFaultInquirerWithNullDataSet ()
		{
			new UngroupedXileInquirer (rawData: null);
		}

		private static IEnumerable<double> GetXiles (int xile, Func<int,double> nThXile)
		{
			for (int nTh = 1; nTh <= xile; nTh++) {
				yield return nThXile (nTh);
			}
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UXileInquirer_Tests
	{
		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedXileInquirer> (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartiles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedXileInquirer> (out sortedData, size: 100);
			
			var expected = GetXiles (4, nTh => HelperMethods.CalcNthXile (sortedData, 4, nTh)).ToList ();
			var actual = GetXiles (4, nTh => calculator.GetQuartile (nTh)).ToList ();

			CollectionAssert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Deciles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedXileInquirer> (out sortedData, size: 100);
			
			var expected = GetXiles (10, nTh => HelperMethods.CalcNthXile (sortedData, 10, nTh)).ToList ();
			var actual = GetXiles (10, nTh => calculator.GetDecile (nTh)).ToList ();

			CollectionAssert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Percentiles ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedXileInquirer> (out sortedData, size: 100);

			var expected = GetXiles (100, nTh => HelperMethods.CalcNthXile (sortedData, 100, nTh)).ToList ();
			var actual = GetXiles (100, nTh => calculator.GetPercentile (nTh)).ToList ();
			
			CollectionAssert.AreEqual (expected, actual);
		}

		private static IEnumerable<double> GetXiles (int xile, Func<int,double> nThXile)
		{
			for (int nTh = 1; nTh <= xile; nTh++) {
				yield return nThXile (nTh);
			}
		}
	}
}
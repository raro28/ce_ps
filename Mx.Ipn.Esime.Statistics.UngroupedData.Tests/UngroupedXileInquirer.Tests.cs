namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Reflection;
	using System.Linq;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.Libs;

	[TestFixture]
	public class UngroupedXileInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedXileInquirer>
	{
		private static Dictionary<Xiles,MethodInfo> xileGetters;

		static UngroupedXileInquirer_Tests ()
		{
			xileGetters = new Dictionary<Xiles,MethodInfo> ()
			{
				{Xiles.Quartile,typeof(UngroupedXileInquirer).GetMethod("GetQuartile")},
				{Xiles.Decile,typeof(UngroupedXileInquirer).GetMethod("GetDecile")},
				{Xiles.Percentile,typeof(UngroupedXileInquirer).GetMethod("GetPercentile")}
			};
		}

		[TestCase(Xiles.Quartile)]
		[TestCase(Xiles.Decile)]
		[TestCase(Xiles.Percentile)]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid (Xiles xile)
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 7);
			var method = xileGetters [xile];

			try {
				method.Invoke (calculator, new object[]{-1});
			} catch (TargetInvocationException ex) {
				exception = ex.InnerException as StatisticsException;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);

			try {
				calculator.GetQuartile ((int)xile + 1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}

		[TestCase(Xiles.Quartile)]
		[TestCase(Xiles.Decile)]
		[TestCase(Xiles.Percentile)]
		public void Inquirer_Gets_Expected_Quartiles (Xiles xile)
		{
			List<double> sortedData;
			var calculator = HelperMethods<UngroupedXileInquirer>.NewInstance (out sortedData, size: 100);
			var method = xileGetters [xile];

			var expected = GetXiles ((int)xile, nTh => HelperMethods<UngroupedXileInquirer>.CalcNthXile (sortedData, (int)xile, nTh)).ToList ();
			var actual = GetXiles ((int)xile, nTh => (double)method.Invoke (calculator, new object[]{nTh})).ToList ();
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
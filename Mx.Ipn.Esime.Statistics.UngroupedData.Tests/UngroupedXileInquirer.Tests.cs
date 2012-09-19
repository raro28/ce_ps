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
		public UngroupedXileInquirer_Tests ()
		{
			InitializeFaultInquirerWithNullDataSet = () => {
				return new UngroupedXileInquirer (rawData: null);};
		}

		[TestCase(Xiles.Quartile, "GetQuartile")]
		[TestCase(Xiles.Decile, "GetDecile")]
		[TestCase(Xiles.Percentile, "GetPercentile")]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Tries_To_Get_Negative (Xiles xile, string methodName)
		{
			var method = typeof(UngroupedXileInquirer).GetMethod (methodName);

			method.Invoke (Helper.NewInstance (size: 7), new object[]{-1});
		}

		[TestCase(Xiles.Quartile, "GetQuartile")]
		[TestCase(Xiles.Decile, "GetDecile")]
		[TestCase(Xiles.Percentile, "GetPercentile")]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Tries_To_Get_Greater (Xiles xile, string methodName)
		{
			var method = typeof(UngroupedXileInquirer).GetMethod (methodName);

			method.Invoke (Helper.NewInstance (size: 7), new object[]{(int)xile + 1});
		}

		[TestCase(Xiles.Quartile, "GetQuartile")]
		[TestCase(Xiles.Decile, "GetDecile")]
		[TestCase(Xiles.Percentile, "GetPercentile")]
		public void Inquirer_Gets_Expected_Quartiles (Xiles xile, string methodName)
		{
			List<double> sortedData;
			var calculator = Helper.NewInstance (out sortedData, size: 100);
			var method = typeof(UngroupedXileInquirer).GetMethod (methodName);

			var expected = GetXiles ((int)xile, nTh => Helper.CalcNthXile (sortedData, (int)xile, nTh)).ToList ();
			var actual = GetXiles ((int)xile, nTh => (double)method.Invoke (calculator, new object[]{nTh})).ToList ();
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
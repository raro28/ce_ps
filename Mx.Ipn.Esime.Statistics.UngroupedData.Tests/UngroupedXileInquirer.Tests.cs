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
		public UngroupedXileInquirer_Tests ():base(()=>{return new UngroupedXileInquirer (rawData: null);})
		{
		}

		[TestCase(Xiles.Quartile,100)]
		[TestCase(Xiles.Decile,100)]
		[TestCase(Xiles.Percentile,100)]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Tries_To_Get_Negative (Xiles xile, int size)
		{
			GetXileMethod (xile).Invoke (Helper.NewInquirer (size), new object[]{-1});
		}

		[TestCase(Xiles.Quartile,100)]
		[TestCase(Xiles.Decile,100)]
		[TestCase(Xiles.Percentile,100)]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Tries_To_Get_Greater (Xiles xile, int size)
		{
			GetXileMethod (xile).Invoke (Helper.NewInquirer (size), new object[]{(int)xile + 1});
		}

		[TestCase(Xiles.Quartile,100)]
		[TestCase(Xiles.Decile,100)]
		[TestCase(Xiles.Percentile,100)]
		public void Inquirer_Gets_Expected (Xiles xile, int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);
			var method = GetXileMethod (xile);

			var expected = GetXiles ((int)xile, nTh => CalcNthXile (sortedData, (int)xile, nTh)).ToList ();
			var actual = GetXiles ((int)xile, nTh => (double)method.Invoke (calculator, new object[]{nTh})).ToList ();
			CollectionAssert.AreEqual (expected, actual);
		}

		protected static IEnumerable<double> GetXiles (int xile, Func<int,double> nThXile)
		{
			for (int nTh = 1; nTh <= xile; nTh++) {
				yield return nThXile (nTh);
			}
		}
		
		protected MethodInfo GetXileMethod (Xiles xile)
		{
			return typeof(UngroupedXileInquirer).GetMethod ("Get" + Enum.GetName (typeof(Xiles), xile));
		}
	}
}
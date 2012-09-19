namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Reflection;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UngroupedRangesInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedRangesInquirer>
	{
		public UngroupedRangesInquirer_Tests ():base(()=>{return new UngroupedRangesInquirer (rawData: null);})
		{
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Data_Range (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = sortedData [sortedData.Count - 1] - sortedData [0];
			var actual = calculator.GetDataRange ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(Xiles.Quartile,100,3,1)]
		[TestCase(Xiles.Decile,100,9,1)]
		[TestCase(Xiles.Percentile,100,90,10)]
		public void Inquirer_Gets_Expected_Range (Xiles xile, int size, int toXile, int fromXile)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = CalcNthXile (sortedData, (int)xile, toXile) - CalcNthXile (sortedData, (int)xile, fromXile);
			var actual = GetInterXileRangeMethod (xile).Invoke (calculator, new object[]{});
			Assert.AreEqual (expected, actual);
		}
		
		protected MethodInfo GetInterXileRangeMethod (Xiles xile)
		{
			return typeof(UngroupedRangesInquirer).GetMethod ("GetInter" + Enum.GetName (typeof(Xiles), xile) + "Range");
		}
	}
}
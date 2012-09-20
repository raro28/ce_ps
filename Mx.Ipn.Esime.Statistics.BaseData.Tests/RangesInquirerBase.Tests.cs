namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Reflection;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	[TestFixture()]
	public abstract class RangesInquirerBase_Tests<TInquirer,THelper>:InquirerBase_Tests<TInquirer,THelper> where TInquirer:RangesInquirerBase where THelper:HelperMethodsBase
	{
		[TestCase(100)]
		public void Inquirer_Gets_Expected_Data_Range (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = SampleDataRange (sortedData);
			var actual = calculator.GetDataRange ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(Xiles.Quartile,100,3,1)]
		[TestCase(Xiles.Decile,100,9,1)]
		[TestCase(Xiles.Percentile,100,90,10)]
		public void Inquirer_Gets_Expected_Range (Xiles xile, int size, int toXile, int fromXile)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = Helper.CalcNthXile (sortedData, (int)xile, toXile) - Helper.CalcNthXile (sortedData, (int)xile, fromXile);
			var actual = GetInterXileRangeMethod (xile).Invoke (calculator, new object[]{});
			Assert.AreEqual (expected, actual);
		}

		protected abstract double SampleDataRange (IList<double> sortedData);
		
		private MethodInfo GetInterXileRangeMethod (Xiles xile)
		{
			return typeof(TInquirer).GetMethod ("GetInter" + Enum.GetName (typeof(Xiles), xile) + "Range");
		}
	}
}
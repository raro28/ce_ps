namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	
	[TestFixture()]
	public abstract class CentralTendecyInquirerBase_Tests<TInquirer,THelper>:InquirerBase_Tests<TInquirer,THelper> where TInquirer:CentralTendecyInquirerBase where THelper:HelperMethodsBase
	{	
		public CentralTendecyInquirerBase_Tests (Func<TInquirer> initializeWithNull):base(initializeWithNull)
		{
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Mean (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = Helper.SampleMean (sortedData);
			var actual = calculator.GetMean ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(new double[]{1,2,3,2})]
		[TestCase(new double[]{6,1,2,3,2,4,5,6})]
		public void Inquirer_Gets_Expected_Mode (double[] dataArray)
		{
			var sortedData = dataArray.ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (ref sortedData);
			
			var <double> expected = SampleMode (sortedData);
			var actual = calculator.GetMode ();
			Assert.AreEqual (expected, actual);
		}
		
		[TestCase(4)]
		[TestCase(5)]
		public void Inquirer_Gets_Expected_Median (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);
			
			var expected = SampleMedian (sortedData);
			var actual = calculator.GetMedian ();
			Assert.AreEqual (expected, actual);
		}

		protected abstract double SampleMedian (IList<double> sortedData);

		protected abstract List<double> SampleMode (IEnumerable<double> sortedData);
	}
}
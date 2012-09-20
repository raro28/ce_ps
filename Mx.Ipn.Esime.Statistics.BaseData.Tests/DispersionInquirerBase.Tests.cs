namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	[TestFixture()]
	public abstract class DispersionInquirerBase_Tests<TInquirer,THelper>:InquirerBase_Tests<TInquirer,THelper> where TInquirer:DispersionInquirerBase where THelper:HelperMethodsBase
	{
		[TestCase(100)]
		public void Inquirer_Gets_Expected_Absolute_Deviation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = SampleAbsoluteDeviation (sortedData, Helper.SampleMean (sortedData));
			var actual = calculator.GetAbsoluteDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Variance (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = SampleVariance (sortedData, Helper.SampleMean (sortedData));
			var actual = calculator.GetVariance ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Standar_Deviation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var expected = Math.Sqrt (SampleVariance (sortedData, Helper.SampleMean (sortedData)));
			var actual = calculator.GetStandarDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Coefficient_Of_Variation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var mean = Helper.SampleMean (sortedData);
			var expected = Math.Sqrt (SampleVariance (sortedData, mean)) / mean;
			var actual = calculator.GetCoefficientOfVariation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase("Kourtosis",100,4,2)]
		[TestCase("Symmetry",100,3,1.5)]
		public void Inquirer_Gets_Expected_Coefficient_Of (string coefficientName, int size, int nthMomentum, double momentum2Pow)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer<TInquirer> (out sortedData, size);

			var mean = Helper.SampleMean (sortedData);
			var expected = SampleMomentum (sortedData, nthMomentum, mean) / Math.Pow (SampleMomentum (sortedData, 2, mean), momentum2Pow);
			var actual = GetCoefficientOfMethod (coefficientName).Invoke (calculator, new object[]{});
			Assert.AreEqual (expected, actual);
		}

		protected abstract double SampleAbsoluteDeviation (List<double> sortedData, double mean);

		protected abstract double SampleVariance (List<double> sortedData, double mean);

		protected abstract double SampleMomentum (List<double> sortedData, int nMomentum, double mean);

		private MethodInfo GetCoefficientOfMethod (string coefficientName)
		{
			return typeof(TInquirer).GetMethod ("GetCoefficientOf" + coefficientName);
		}
	}
}
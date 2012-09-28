namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Linq;
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
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);

			var expected = SampleAbsoluteDeviation (data, Helper.SampleMean (data));
			var actual = calculator.GetAbsoluteDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Variance (int size)
		{
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);

			var expected = SampleVariance (data, Helper.SampleMean (data));
			var actual = calculator.GetVariance ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Standar_Deviation (int size)
		{
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);

			var expected = Math.Sqrt (SampleVariance (data, Helper.SampleMean (data)));
			var actual = calculator.GetStandarDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Coefficient_Of_Variation (int size)
		{
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);

			var mean = Helper.SampleMean (data);
			var expected = Math.Sqrt (SampleVariance (data, mean)) / mean;
			var actual = calculator.GetCoefficientOfVariation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase("Kourtosis",100,4,2)]
		[TestCase("Symmetry",100,3,1.5)]
		public void Inquirer_Gets_Expected_Coefficient_Of (string coefficientName, int size, int nthMomentum, double momentum2Pow)
		{
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);

			var mean = Helper.SampleMean (data);
			var expected = SampleMomentum (data, nthMomentum, mean) / Math.Pow (SampleMomentum (data, 2, mean), momentum2Pow);
			var actual = GetCoefficientOfMethod (coefficientName).Invoke (calculator, new object[]{});
			Assert.AreEqual (expected, actual);
		}

		protected abstract double SampleAbsoluteDeviation (List<double> data, double mean);

		protected abstract double SampleVariance (List<double> data, double mean);

		protected abstract double SampleMomentum (List<double> data, int nMomentum, double mean);

		private MethodInfo GetCoefficientOfMethod (string coefficientName)
		{
			return typeof(TInquirer).GetMethod ("GetCoefficientOf" + coefficientName);
		}
	}
}
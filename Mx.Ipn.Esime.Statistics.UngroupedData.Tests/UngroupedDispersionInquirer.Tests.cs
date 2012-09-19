namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UngroupedDispersionInquirer_Tests:UngroupedInquirerBase_Tests<UngroupedDispersionInquirer>
	{
		public UngroupedDispersionInquirer_Tests ():base(()=>{return new UngroupedDispersionInquirer (rawData: null);})
		{
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Absolute_Deviation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = SampleAbsoluteDeviation (sortedData, SampleMean (sortedData));
			var actual = calculator.GetAbsoluteDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Variance (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = SampleVariance (sortedData, SampleMean (sortedData));
			var actual = calculator.GetVariance ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Standar_Deviation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var expected = Math.Sqrt (SampleVariance (sortedData, SampleMean (sortedData)));
			var actual = calculator.GetStandarDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase(100)]
		public void Inquirer_Gets_Expected_Coefficient_Of_Variation (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var mean = SampleMean (sortedData);
			var expected = Math.Sqrt (SampleVariance (sortedData, mean)) / mean;
			var actual = calculator.GetCoefficientOfVariation ();
			Assert.AreEqual (expected, actual);
		}

		[TestCase("Kourtosis",100,4,2)]
		[TestCase("Symmetry",100,3,1.5)]
		public void Inquirer_Gets_Expected_Coefficient_Of (string coefficientName, int size, int nthMomentum, double momentum2Pow)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);

			var mean = SampleMean (sortedData);
			var expected = SampleMomentum (sortedData, nthMomentum, mean) / Math.Pow (SampleMomentum (sortedData, 2, mean), momentum2Pow);
			var actual = GetCoefficientOfMethod (coefficientName).Invoke (calculator, new object[]{});
			Assert.AreEqual (expected, actual);
		}

		protected double SampleAbsoluteDeviation (List<double> sortedData, double mean)
		{
			var nAbsoluteDeviation = 0.0;
			sortedData.ForEach (item => nAbsoluteDeviation += Math.Abs (item - mean));
			var absoluteDeviation = nAbsoluteDeviation / sortedData.Count;
			
			return absoluteDeviation;
		}

		protected double SampleVariance (List<double> sortedData, double mean)
		{
			var nplus1Variance = 0.0;
			sortedData.ForEach (item => nplus1Variance += Math.Pow ((item - mean), 2));
			var variance = nplus1Variance / (sortedData.Count - 1);

			return variance;
		}

		protected double SampleMomentum (List<double> sortedData, int nMomentum, double mean)
		{
			var sum = 0.0;
			sortedData.ForEach (item => sum += Math.Pow ((item - mean), nMomentum));

			var momentum = sum / sortedData.Count;

			return momentum;
		}

		private MethodInfo GetCoefficientOfMethod (string coefficientName)
		{
			return typeof(UngroupedDispersionInquirer).GetMethod ("GetCoefficientOf" + coefficientName);
		}
	}
}
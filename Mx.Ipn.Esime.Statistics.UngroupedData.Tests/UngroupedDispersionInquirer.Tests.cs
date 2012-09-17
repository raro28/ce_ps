namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;

	[TestFixture()]
	public class UngroupedDispersionInquirer_Tests
	{
		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void Inquirer_Gets_Expected_Absolute_Deviation ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Absolute_Deviation (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Absolute_Deviation (List<double> sortedData, dynamic calculator)
		{
			var mean = SampleMean (sortedData);
			var nAbsoluteDeviation = 0.0;
			sortedData.ForEach (item => nAbsoluteDeviation += Math.Abs (item - mean));
			var expected = nAbsoluteDeviation / sortedData.Count;
			var actual = calculator.GetAbsoluteDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Variance ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Variance (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Variance (List<double> sortedData, dynamic calculator)
		{
			var expected = SampleVariance (sortedData);
			var actual = calculator.GetVariance ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Standar_Deviation ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Standar_Deviation (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Standar_Deviation (List<double> sortedData, dynamic calculator)
		{
			var expected = SampleStandarDeviation (sortedData);
			var actual = calculator.GetStandarDeviation ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Variation ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Coefficient_Of_Variation (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Coefficient_Of_Variation (List<double> sortedData, dynamic calculator)
		{
			var strDev = SampleStandarDeviation (sortedData);
			var mean = SampleMean (sortedData);
			var expected = strDev / mean;
			var actual = calculator.GetCoefficientOfVariation ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Symmetry ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Coefficient_Of_Symmetry (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Coefficient_Of_Symmetry (List<double> sortedData, dynamic calculator)
		{
			var m3 = SampleMomentum (sortedData, 3);
			var m2 = SampleMomentum (sortedData, 2);
			var expected = m3 / Math.Pow (m2, 1.5);
			var actual = calculator.GetCoefficientOfSymmetry ();
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Kurtosis ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<UngroupedDispersionInquirer> (out sortedData, size: 100);

			Inquirer_Gets_Expected_Coefficient_Of_Kurtosis (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Coefficient_Of_Kurtosis (List<double> sortedData, dynamic calculator)
		{
			var m4 = SampleMomentum (sortedData, 4);
			var m2 = SampleMomentum (sortedData, 2);
			var expected = m4 / Math.Pow (m2, 2);
			var actual = calculator.GetCoefficientOfKourtosis ();
			Assert.AreEqual (expected, actual);
		}

		private static double SampleMean (ICollection<double> sortedData)
		{
			var mean = sortedData.Sum () / sortedData.Count;

			return mean;
		}

		private static double SampleVariance (List<double> sortedData)
		{
			var mean = SampleMean (sortedData);
			var nplus1Variance = 0.0;
			sortedData.ForEach (item => nplus1Variance += Math.Pow ((item - mean), 2));
			var variance = nplus1Variance / (sortedData.Count - 1);

			return variance;
		}

		private static double SampleStandarDeviation (List<double> sortedData)
		{
			var variance = SampleVariance (sortedData);
			var stDeviation = Math.Sqrt (variance);

			return stDeviation;
		}

		private static double SampleMomentum (List<double> sortedData, int nMomentum)
		{
			var mean = SampleMean (sortedData);
			var sum = 0.0;
			sortedData.ForEach (item => sum += Math.Pow ((item - mean), nMomentum));

			var momentum = sum / sortedData.Count;
			return momentum;
		}
	}
}
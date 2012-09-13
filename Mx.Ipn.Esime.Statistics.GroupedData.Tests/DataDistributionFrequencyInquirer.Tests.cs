namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;

	[TestFixture()]
	public class DataDistributionFrequencyInquirer_Tests
	{
		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void Inquirer_Gets_Expected_Frequencies ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Frequencies (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Frequencies(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleFrequencies(sortedData);
			var actual = calculator.GetFrequencies();
			
			CollectionAssert.AreEqual(expected,actual);
		}

		private static IEnumerable<double> SampleFrequencies (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Acumulated_Frequencies ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Acumulated_Frequencies (sortedData, calculator);
		}

		public static void Inquirer_Gets_Expected_Acumulated_Frequencies(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleAcumulatedFrequencies(sortedData);
			var actual = calculator.GetAcumulatedFrequencies();

			CollectionAssert.AreEqual(expected,actual);
		}

		private static IEnumerable<double> SampleAcumulatedFrequencies (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Relative_Frequencies ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Relative_Frequencies (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Relative_Frequencies(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleRelative_Frequencies(sortedData);
			var actual = calculator.GetRelativeFrequencies();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private static IEnumerable<double> SampleRelative_Frequencies (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Acumulated_Relative_Frequencies ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Acumulated_Relative_Frequencies (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Acumulated_Relative_Frequencies(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleAcumulatedRelativeFrequencies(sortedData);
			var actual = calculator.GetAcumulatedRelativeFrequencies();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private static IEnumerable<double> SampleAcumulatedRelativeFrequencies (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Class_Marks ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Class_Marks (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Class_Marks(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleClassMarks(sortedData);
			var actual = calculator.GetClassMarks();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private static IEnumerable<double> SampleClassMarks (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Class_Intervals ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Class_Intervals (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Class_Intervals(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleClassIntervals(sortedData);
			var actual = calculator.GetClassIntervals();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private static IEnumerable<double> SampleClassIntervals (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		[Test()]
		public void Inquirer_Gets_Expected_Real_Intervals ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<DataDistributionFrequencyInquirer> (out sortedData, size: 100);
			
			Inquirer_Gets_Expected_Real_Intervals (sortedData, calculator);
		}
		
		public static void Inquirer_Gets_Expected_Real_Intervals(List<double> sortedData, dynamic calculator)
		{
			var expected = SampleRealIntervals(sortedData);
			var actual = calculator.GetRealsIntervals();
			
			CollectionAssert.AreEqual(expected,actual);
		}
		
		private static IEnumerable<double> SampleRealIntervals (List<double> sortedData)
		{
			throw new NotImplementedException ();
		}
	}
}


namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.GroupedData;

	[TestFixture()]
	public class GroupedStatisticsInquirer_Integration_Tests
	{
		[Test()]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				var calculator = new GroupedStatisticsInquirer (rawData: null);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(ArgumentNullException), exception.InnerException);
		}
		
		[Test()]
		public void When_Inquirer_Recieves_Empty_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			var emptyList = new List<double> ();
			try {
				var calculator = new GroupedStatisticsInquirer (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);	
		}
		
		[Test()]
		public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			var emptyList = new List<double>{1};
			try {
				var calculator = new GroupedStatisticsInquirer (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
		}

		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void Inquirer_Gets_Expected_Data_Range ()
		{
			List<double> sortedData;

			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedRangesInquirer_Tests.Inquirer_Gets_Expected_Data_Range (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedRangesInquirer_Tests.Inquirer_Gets_Expected_Quartil_Decil_Percentil_Ranges (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Mean ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedCentralTendecyInquirer_Tests.Inquirer_Gets_Expected_Mean (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Mode ()
		{
			List<double> sortedData = new List<double>{1,2,3,2};
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (ref sortedData);
			GroupedCentralTendecyInquirer_Tests.Inquirer_Gets_Expected_Mode (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Median ()
		{
			List<double> sortedData = new List<double>{1,2,3};
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (ref sortedData);
			GroupedCentralTendecyInquirer_Tests.Inquirer_Gets_Expected_Median (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Quartiles ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedXileInquirer_Tests.Inquirer_Gets_Expected_Quartiles (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Deciles ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedXileInquirer_Tests.Inquirer_Gets_Expected_Deciles (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Percentiles ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedXileInquirer_Tests.Inquirer_Gets_Expected_Percentiles (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Absolute_Deviation ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Absolute_Deviation (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Variance ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Variance (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Standar_Deviation ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Standar_Deviation (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Variation ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Standar_Deviation (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Symmetry ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Coefficient_Of_Symmetry (sortedData, inquirer);
		}

		[Test()]
		public void Inquirer_Gets_Expected_Coefficient_Of_Kurtosis ()
		{
			List<double> sortedData;
			
			var inquirer = HelperMethods.NewInstanceOf<GroupedStatisticsInquirer> (out sortedData, 100);
			GroupedDispersionInquirer_Tests.Inquirer_Gets_Expected_Coefficient_Of_Symmetry (sortedData, inquirer);
		}
	}
}
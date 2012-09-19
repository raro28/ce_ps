namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.GroupedData;

	[TestFixture()]
	public class GroupedXileInquirer_Excepton_Tests
	{
		[Test()]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				var calculator = new GroupedXileInquirer (rawData: null);
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
				var calculator = new GroupedXileInquirer (emptyList);
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
				var calculator = new GroupedXileInquirer (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
		}

		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Quartiles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedXileInquirer> (out sortedData, size: 7);
			try {
				calculator.GetQuartile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);

			try {
				calculator.GetQuartile (5);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}

		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Deciles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedXileInquirer> (out sortedData, size: 7);
			try {
				calculator.GetDecile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);

			try {
				calculator.GetDecile (11);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}

		[Test()]
		public void When_Inquirer_Recieves_Tries_To_Get_Invalid_Percentiles ()
		{
			StatisticsException exception = null;
			List<double> sortedData;
			var calculator = HelperMethods.NewInstanceOf<GroupedXileInquirer> (out sortedData, size: 7);
			try {
				calculator.GetPercentile (-1);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);

			try {
				calculator.GetPercentile (101);
			} catch (StatisticsException ex) {
				exception = ex;
			}
			
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(IndexOutOfRangeException), exception.InnerException);
		}
	}
}
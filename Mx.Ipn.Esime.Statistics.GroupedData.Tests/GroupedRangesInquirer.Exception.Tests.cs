namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	
	[TestFixture()]
	public class GroupedRangesInquirer_Exception_Tests
	{
		[Test()]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				var calculator = new GroupedRangesInquirer (rawData:null);
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
				var calculator = new GroupedRangesInquirer (emptyList);
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
				var calculator = new GroupedRangesInquirer (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}

			Assert.IsNotNull (exception);
		}
	}
}
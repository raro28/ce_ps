namespace Mx.Ipn.Esime.Statistics.Tests
{
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	
	[TestFixture()]
	public class UCentralTendecyCalculator_Exception_Tests
	{
		[Test()]
		public void When_Calculator_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				var calculator = new UCentralTendecyCalculator (sortedData: null);
			} catch (StatisticsException ex) {
				exception = ex;
			}

			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(ArgumentNullException), exception.InnerException);
		}

		[Test()]
		public void When_Calculator_Recieves_Empty_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			var emptyList = new List<double> ();
			try {
				var calculator = new UCentralTendecyCalculator (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}

			Assert.IsNotNull (exception);	
		}

		[Test()]
		public void When_Calculator_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			var emptyList = new List<double>{1};
			try {
				var calculator = new UCentralTendecyCalculator (emptyList);
			} catch (StatisticsException ex) {
				exception = ex;
			}

			Assert.IsNotNull (exception);
		}
	}
}
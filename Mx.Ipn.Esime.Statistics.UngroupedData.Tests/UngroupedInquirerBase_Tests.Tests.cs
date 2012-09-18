namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Libs;

	[TestFixture()]
	public abstract class UngroupedInquirerBase_Tests<T>
	{
		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = HelperMethods<T>.NewInstance (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], calculator.Data [i]);
			}
		}

		[Test()]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				InitializeFaultInquirerWithNullDataSet ();
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
			try {
				Activator.CreateInstance (typeof(T), new Object[]{new List<double> ()});
			} catch (TargetInvocationException ex) {
				exception = ex.InnerException as StatisticsException;
			}
			
			Assert.IsNotNull (exception);	
		}
		
		[Test()]
		public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception ()
		{
			StatisticsException exception = null;
			try {
				Activator.CreateInstance (typeof(T), new Object[]{new List<double>{1}});
			} catch (TargetInvocationException ex) {
				exception = ex.InnerException  as StatisticsException;
			}
			
			Assert.IsNotNull (exception);
		}

		protected abstract void InitializeFaultInquirerWithNullDataSet ();
	}
}
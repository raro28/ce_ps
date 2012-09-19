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
		protected HelperMethods<T> Helper {
			get;
			set;
		}

		protected Func<T> InitializeFaultInquirerWithNullDataSet {
			get;
			set;
		}

		public UngroupedInquirerBase_Tests ()
		{
			Helper = new HelperMethods<T> ();
		}

		[Test()]
		public void Inquirer_Uses_Internal_Sorted_Data_Set ()
		{
			List<double> sortedData;
			var calculator = Helper.NewInstance (out sortedData, size: 100);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], ((dynamic)calculator).Data [i]);
			}
		}

		[Test()]
		[ExpectedException(typeof(StatisticsException),Handler="HandleExceptionWithInnerArgumentNullException")]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			//TODO:Fixme
			InitializeFaultInquirerWithNullDataSet ();
		}
		
		[Test()]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Empty_Data_Set_Throws_An_Statistics_Exception ()
		{
			Activator.CreateInstance (typeof(T), new Object[]{new List<double> ()});	
		}
		
		[Test()]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception ()
		{
			Activator.CreateInstance (typeof(T), new Object[]{new List<double>{1}});
		}

		protected void HandleExceptionWithInnerArgumentNullException (Exception exception)
		{
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(StatisticsException), exception);
			Assert.IsInstanceOfType (typeof(ArgumentNullException), exception.InnerException);
		}

		protected void HandleExceptionThroughTargetInvocationExceptionException (Exception exception)
		{
			Assert.IsNotNull (exception);
			Assert.IsInstanceOfType (typeof(TargetInvocationException), exception);
		}
	}
}
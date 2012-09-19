namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	[TestFixture()]
	public abstract class InquirerBase_Tests<T> where T:InquirerBase
	{
		protected HelperMethodsBase<T> Helper {
			get;
			private set;
		}

		private readonly Func<T> InitializeFaultInquirerWithNullDataSet;

		//FIXME better way to init inquirer with null data set
		public InquirerBase_Tests (Func<T> initializeWithNull, HelperMethodsBase<T> helper)
		{
			InitializeFaultInquirerWithNullDataSet = initializeWithNull;
			Helper = helper;
		}

		[TestCase(100)]
		public void Inquirer_Uses_Internal_Sorted_Data_Set (int size)
		{
			List<double> sortedData;
			var calculator = Helper.NewInquirer (out sortedData, size);
			
			for (int i = 0; i < sortedData.Count; i++) {
				Assert.AreEqual (sortedData [i], ((dynamic)calculator).Data [i]);
			}
		}

		[Test()]
		[ExpectedException(typeof(StatisticsException),Handler="HandleExceptionWithInnerArgumentNullException")]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			//FIXME init inquirer with null data set
			InitializeFaultInquirerWithNullDataSet ();
		}
		
		[TestCase(0)]
		[TestCase(1)]
		[ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
		public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception (int size)
		{
			Helper.NewInquirer (size);
		}

		protected void HandleExceptionWithInnerArgumentNullException (Exception exception)
		{
			Assert.IsInstanceOf<ArgumentNullException> (exception.InnerException);
		}

		protected void HandleExceptionThroughTargetInvocationExceptionException (Exception exception)
		{
			Assert.IsInstanceOf<StatisticsException> (exception.InnerException);
		}
	}
}
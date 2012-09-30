namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.Core;
	using Mx.Ipn.Esime.Statistics.Core.Base;
	
	[TestFixture()]
	public abstract class InquirerBase_Tests<TInquirer,THelper> where TInquirer:InquirerBase where THelper:HelperMethodsBase
	{
		protected THelper Helper {
			get;
			set;
		}
		public InquirerBase_Tests ()
		{
			Helper = (THelper)Activator.CreateInstance (typeof(THelper), new object[]{});
		}

		[TestCase(100)]
		public void Inquirer_Uses_Internal_Sorted_Data_Set (int size)
		{
			List<double> data = Helper.GetRandomDataSample (size).ToList ();
			var calculator = Helper.NewInquirer<TInquirer> (data);
			
			for (int i = 0; i < data.Count; i++) {
				Assert.AreEqual (data [i], ((dynamic)calculator).Data [i]);
			}
		}

		[TestCase(new double[]{1,2,3},0)]
		[TestCase(new double[]{1,2.1,3.2},1)]
		[TestCase(new double[]{1,2.1,3.21},2)]
		public void Inquirer_Uses_Correct_Data_Precision_Value (double[]data, int orecisionValue)
		{
			dynamic calculator = Helper.NewInquirer<TInquirer> (data.ToList ());
			
			Assert.AreEqual (calculator.DataPrecision, orecisionValue);
		}

		[Test()]
		[ExpectedException(typeof(StatisticsException),Handler="HandleExceptionWithInnerArgumentNullException")]
		public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception ()
		{
			Helper.NewInquirer<TInquirer> (data: null);
		}
		
		[TestCase(0)]
		[TestCase(1)]
		[ExpectedException(typeof(StatisticsException))]
		public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception (int size)
		{
			Helper.NewInquirer<TInquirer> (size: size);
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
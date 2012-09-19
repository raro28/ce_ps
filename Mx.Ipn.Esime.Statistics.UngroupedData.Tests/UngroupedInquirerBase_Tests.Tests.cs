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
			private set;
		}

		private readonly Func<T> InitializeFaultInquirerWithNullDataSet;

		//TODO:fixme
		public UngroupedInquirerBase_Tests (Func<T> initializeWithNull)
		{
			InitializeFaultInquirerWithNullDataSet = initializeWithNull;
			Helper = new HelperMethods<T> ();
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
			//TODO:Fixme
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
			Assert.IsInstanceOfType (typeof(ArgumentNullException), exception.InnerException);
		}

		protected void HandleExceptionThroughTargetInvocationExceptionException (Exception exception)
		{
			Assert.IsInstanceOfType (typeof(StatisticsException), exception.InnerException);
		}


		protected double CalcNthXile (IList<double> data, int xile, int nTh)
		{
			var lx = data.Count * nTh / (double)xile;
			var li = (int)Math.Floor (lx - 0.5);
			var ls = (int)Math.Floor (lx + 0.5);
			if (ls == data.Count) {
				ls = li;
			}
			var iPortion = li + 1 - (lx - 0.5);
			var sPortion = 1 - iPortion;
			var xRange = iPortion * data [li] + sPortion * data [ls];
			
			return xRange;
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
    using NUnit.Framework;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using System;

    [TestFixture()]
    public class DataContainer_Tests :TestsBase<HelperMethods>
    {
        [TestCase(100)]
        public void Inquirer_Uses_Internal_Sorted_Data_Set(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var container = Helper.NewInquirer<DataContainer>(data);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(data[i], container.Data[i]);
            }
        }
        
        [TestCase(new double[]{1,2,3},0)]
        [TestCase(new double[]{1,2.1,3.2},1)]
        [TestCase(new double[]{1,2.1,3.21},2)]
        public void Inquirer_Uses_Correct_Data_Precision_Value(double[]data, int dataPrecision)
        {
            var container = Helper.NewInquirer<DataContainer>(data.ToList());
            
            Assert.AreEqual(container.DataPrecision, dataPrecision);
        }
        
        [Test()]
        [ExpectedException(typeof(StatisticsException),Handler="HandleExceptionWithInnerArgumentNullException")]
        public void When_Inquirer_Recieves_Null_Data_Set_Throws_An_Statistics_Exception()
        {
            Helper.NewInquirer<DataContainer>(data: null);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [ExpectedException(typeof(StatisticsException))]
        public void When_Inquirer_Recieves_Less_Than_Two_Elements_Data_Set_Throws_An_Statistics_Exception(int size)
        {
            Helper.NewInquirer<DataContainer>(size: size);
        }
        
        protected void HandleExceptionWithInnerArgumentNullException(Exception exception)
        {
            Assert.IsInstanceOf<ArgumentNullException>(exception.InnerException);
        }
    }
}


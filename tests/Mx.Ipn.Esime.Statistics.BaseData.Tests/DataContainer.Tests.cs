/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
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
        public void Inquirer_Gets_Correct_Decimals(double[]data, int dataPrecision)
        {
            var container = Helper.NewInquirer<DataContainer>(data.ToList());
            
            Assert.AreEqual(container.DataPrecision, dataPrecision);
        }

        [TestCase(new double[]{1,2,3},0.5)]
        [TestCase(new double[]{1,2.1,3.2},0.1)]
        [TestCase(new double[]{1,2.1,3.21},0.01)]
        public void Inquirer_Uses_Correct_Data_Precision_Value(double[]data, double dataPrecision)
        {
            var container = Helper.NewInquirer<DataContainer>(data.ToList());
            
            Assert.AreEqual(container.DataPrecisionValue, dataPrecision);
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


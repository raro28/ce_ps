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
	using System;
    using System.Reflection;
    using System.Linq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    [TestFixture]
    public abstract class XileInquirerBase_Tests<TInquirer,THelper> : TestsBase<THelper> where TInquirer:XileInquirerBase where THelper:HelperMethods
    {
        [TestCase(Xiles.Quartile,100)]
        [TestCase(Xiles.Decile,100)]
        [TestCase(Xiles.Percentile,100)]
        [ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
        public void When_Inquirer_Recieves_Tries_To_Get_Negative(Xiles xile, int size)
        {
            GetXileMethod(xile).Invoke(Helper.NewInquirer<TInquirer>(size: size), new object[]{-1});
        }

        [TestCase(Xiles.Quartile,100)]
        [TestCase(Xiles.Decile,100)]
        [TestCase(Xiles.Percentile,100)]
        [ExpectedException(typeof(TargetInvocationException),Handler="HandleExceptionThroughTargetInvocationExceptionException")]
        public void When_Inquirer_Recieves_Tries_To_Get_Greater(Xiles xile, int size)
        {
            GetXileMethod(xile).Invoke(Helper.NewInquirer<TInquirer>(size: size), new object[]{(int)xile + 1});
        }

        [TestCase(Xiles.Quartile,100)]
        [TestCase(Xiles.Decile,100)]
        [TestCase(Xiles.Percentile,100)]
        public void Inquirer_Gets_All_Expected(Xiles xile, int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);
            var method = GetXileMethod(xile);

            var expected = GetXiles((int)xile, nTh => Helper.CalcNthXile(data, (int)xile, nTh)).ToList();
            var actual = GetXiles((int)xile, nTh => (double)method.Invoke(calculator, new object[]{nTh})).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        protected void HandleExceptionThroughTargetInvocationExceptionException(Exception exception)
        {
            Assert.IsInstanceOf<StatisticsException>(exception.InnerException);
        }

        private IEnumerable<double> GetXiles(int xile, Func<int,double> nThXile)
        {
            for (int nTh = 1; nTh <= xile; nTh++)
            {
                yield return nThXile(nTh);
            }
        }
		
        private MethodInfo GetXileMethod(Xiles xile)
        {
            return typeof(TInquirer).GetMethod("Get" + Enum.GetName(typeof(Xiles), xile));
        }
    }
}
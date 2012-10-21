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
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    [TestFixture()]
    public abstract class DispersionInquirerBase_Tests<TInquirer,THelper> : TestsBase<THelper> where TInquirer:DispersionInquirerBase where THelper:HelperMethods
    {
        [TestCase(100)]
        public void Inquirer_Gets_Expected_Data_Range(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);
			
            var expected = SampleDataRange(data);
            var actual = calculator.GetDataRange();
            Assert.AreEqual(expected, actual);
        }
		
        [TestCase(Xiles.Quartile,100,3,1)]
        [TestCase(Xiles.Decile,100,9,1)]
        [TestCase(Xiles.Percentile,100,90,10)]
        public void Inquirer_Gets_Expected_Range(Xiles xile, int size, int toXile, int fromXile)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);
			
            var expected = Helper.CalcNthXile(data, (int)xile, toXile) - Helper.CalcNthXile(data, (int)xile, fromXile);
            var actual = GetInterXileRangeMethod(xile).Invoke(calculator, new object[]{});
            Assert.AreEqual(expected, actual);
        }

        [TestCase(100)]
        public void Inquirer_Gets_Expected_Absolute_Deviation(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);

            var expected = SampleAbsoluteDeviation(data);
            var actual = calculator.GetAbsoluteDeviation();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(100)]
        public void Inquirer_Gets_Expected_Variance(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);

            var expected = SampleVariance(data);
            var actual = calculator.GetVariance();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(100)]
        public void Inquirer_Gets_Expected_Standar_Deviation(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);

            var expected = Math.Sqrt(SampleVariance(data));
            var actual = calculator.GetStandarDeviation();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(100)]
        public void Inquirer_Gets_Expected_Coefficient_Of_Variation(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);

            var mean = Helper.SampleMean(data);
            var expected = Math.Sqrt(SampleVariance(data)) / mean;
            var actual = calculator.GetCoefficientOfVariation();
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Kourtosis",100,4,2)]
        [TestCase("Symmetry",100,3,1.5)]
        public void Inquirer_Gets_Expected_Coefficient_Of(string coefficientName, int size, int nthMomentum, double momentum2Pow)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);

            var expected = SampleMomentum(data, nthMomentum) / Math.Pow(SampleMomentum(data, 2), momentum2Pow);
            var actual = GetCoefficientOfMethod(coefficientName).Invoke(calculator, new object[]{});
            Assert.AreEqual(expected, actual);
        }

        protected abstract double SampleAbsoluteDeviation(List<double> data);

        protected abstract double SampleVariance(List<double> data);

        protected abstract double SampleMomentum(List<double> data, int nMomentum);
		
        protected abstract double SampleDataRange(IList<double> data);
		
        private MethodInfo GetInterXileRangeMethod(Xiles xile)
        {
            return typeof(TInquirer).GetMethod("GetInter" + Enum.GetName(typeof(Xiles), xile) + "Range");
        }

        private MethodInfo GetCoefficientOfMethod(string coefficientName)
        {
            return typeof(TInquirer).GetMethod("GetCoefficientOf" + coefficientName);
        }
    }
}
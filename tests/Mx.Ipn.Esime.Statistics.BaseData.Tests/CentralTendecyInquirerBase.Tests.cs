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
    using System.Collections.Generic;
    using NUnit.Framework;
    using Mx.Ipn.Esime.Statistics.Core;
	
    [TestFixture()]
    public abstract class CentralTendecyInquirerBase_Tests<TInquirer,THelper> : TestsBase<THelper> where TInquirer:ICentralTendencyInquirer where THelper:HelperMethods
    {
        [TestCase(100)]
        public void Inquirer_Gets_Expected_Mean(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);
            data.Sort();

            var expected = Helper.SampleMean(data);
            var actual = calculator.GetMean();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new double[]{1,2,3,2})]
        [TestCase(new double[]{6,1,2,3,2,4,5,6})]
        public void Inquirer_Gets_Expected_Mode(IEnumerable<double> data)
        {
            var _data = data.ToList();
            var calculator = Helper.NewInquirer<TInquirer>(_data);
			
            var expected = SampleMode(_data);
            var actual = calculator.GetModes();
            Assert.AreEqual(expected, actual);
        }
		
        [TestCase(4)]
        [TestCase(5)]
        public void Inquirer_Gets_Expected_Median(int size)
        {
            var data = Helper.GetRandomDataSample(size).ToList();
            var calculator = Helper.NewInquirer<TInquirer>(data);
			
            var expected = SampleMedian(data);
            var actual = calculator.GetMedian();
            Assert.AreEqual(expected, actual);
        }

        protected abstract double SampleMedian(IList<double> data);

        protected abstract List<double> SampleMode(IEnumerable<double> data);
    }
}
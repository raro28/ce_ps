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
namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;

    public class UngroupedHelperMethods:HelperMethods
    {
        public UngroupedHelperMethods()
        {
            this.CalcNthXile = (data, xile, nTh) => {
                var lx = data.Count * nTh / (double)xile;
                var li = (int)Math.Floor(lx - 0.5);
                var ls = (int)Math.Floor(lx + 0.5);
                if (ls == data.Count)
                {
                    ls = li;
                }
                var iPortion = li + 1 - (lx - 0.5);
                var sPortion = 1 - iPortion;
                var xRange = iPortion * data[li] + sPortion * data[ls];
                
                return xRange;
            };

            this.SampleMean = data => {
                var sum = 0.0;
                data.ForEach(item => sum += item);
                var mean = sum / data.Count;
                
                return mean;
            };
        }

        public override TInquirer NewInquirer<TInquirer>(Ninject.StandardKernel kernel)
        {
            kernel
                .Bind<UngroupedXileInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<UngroupedCentralTendecyInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<UngroupedDispersionInquirer>()
                    .ToSelf().InSingletonScope();
            
            return kernel.Get<TInquirer>();
        }
    }
}
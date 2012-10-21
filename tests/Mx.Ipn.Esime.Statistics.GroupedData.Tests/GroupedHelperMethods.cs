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
namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;
    using NUnit.Framework;
	
    [Ignore("appharbor webdeploy")]
    public class GroupedHelperMethods:HelperMethods
    {
        public GroupedHelperMethods()
        {
            this.CalcNthXile = (data, xile, nTh) => {
                //TODO:GroupedHelperMethods:CalcNthXile
                return -1;
            };
            
            this.SampleMean = data => {
                //TODO:GroupedHelperMethods:SampleMean
                return -1;
            };
        }

        public override TInquirer NewInquirer<TInquirer>(Ninject.StandardKernel kernel)
        {
            kernel
                .Bind<DataDistributionFrequencyInquirer>()
                .ToSelf().InSingletonScope();
            kernel
                .Bind<GroupedXileInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<GroupedCentralTendecyInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<GroupedDispersionInquirer>()
                    .ToSelf().InSingletonScope();

            return kernel.Get<TInquirer>();
        }
    }
}
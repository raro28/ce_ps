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
    using NUnit.Framework;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Ninject;

    [TestFixture()]
    public class POC
    {
        [Test()]
        public void TestCase()
        {
            StandardKernel Kernel = new StandardKernel();
            var helper = new UngroupedHelperMethods();

            Kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(helper.GetRandomDataSample(500))).InSingletonScope();
            Kernel.Bind<UngroupedXileInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<UngroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<UngroupedDispersionInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<UngroupedStatisticsInquirer>().ToSelf().InSingletonScope().Named("Singleton");

            dynamic inquirer = Kernel.Get<UngroupedStatisticsInquirer>("Singleton");
            var cok = inquirer.GetCoefficientOfKourtosis();
            var mean = inquirer.GetModes();
            var percentile34 = inquirer.GetPercentile(34);
        }
    }
}


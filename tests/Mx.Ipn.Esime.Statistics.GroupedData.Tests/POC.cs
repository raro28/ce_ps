using NUnit.Framework;
using Mx.Ipn.Esime.Statistics.Core.Base;
using Ninject;

namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
    [Ignore("appharbor webdeploy")]
    [TestFixture()]
    public class POC
    {
        [Test()]
        public void TestCase()
        {
            StandardKernel Kernel = new StandardKernel();
            var helper = new GroupedHelperMethods();

            Kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(helper.GetRandomDataSample(100))).InSingletonScope();
            Kernel.Bind<GroupedXileInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedDispersionInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<DataDistributionFrequencyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedStatisticsInquirer>().ToSelf().InSingletonScope().Named("Singleton");


            dynamic inquirer = Kernel.Get<GroupedStatisticsInquirer>("Singleton");
            var cok = inquirer.GetCoefficientOfKourtosis();
            var modes = inquirer.GetModes();
            var percentile34 = inquirer.GetPercentile(34);
            cok = inquirer.GetCoefficientOfKourtosis();
            var mean = inquirer.GetMean();
            inquirer.AddMeanDifference(4);
            inquirer.AddMeanDifference(3);

            var container = Kernel.Get<DataContainer>();
        }
    }
}


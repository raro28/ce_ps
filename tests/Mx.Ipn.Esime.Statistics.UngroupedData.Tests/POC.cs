using NUnit.Framework;
using Mx.Ipn.Esime.Statistics.Core.Base;
using Ninject;

namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
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


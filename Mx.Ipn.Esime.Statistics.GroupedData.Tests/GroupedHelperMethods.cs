namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;
	
    public class GroupedHelperMethods:HelperMethodsBase
    {
        public override double CalcNthXile(IList<double> data, int xile, int nTh)
        {
            //TODO:GroupedHelperMethods:CalcNthXile
            return -1;
        }		

        public override double SampleMean(List<double> data)
        {
            //TODO:GroupedHelperMethods:SampleMean
            return -1;
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
namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;
	
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
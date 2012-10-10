namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.GroupedData;

    public class GroupedStatisticsInquirer : StatisticsInquirerBase
    {
        static GroupedStatisticsInquirer()
        {
            Kernel.Bind<DataDistributionFrequencyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedXileInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<GroupedDispersionInquirer>().ToSelf().InSingletonScope();
        }

        public GroupedStatisticsInquirer(DataContainer dataContainer, DataDistributionFrequencyInquirer frequencyInquirer, GroupedXileInquirer xileInquirer, GroupedCentralTendecyInquirer centralTendecyInquirer, GroupedDispersionInquirer dispersionInquirer) : base(dataContainer, frequencyInquirer, xileInquirer, centralTendecyInquirer, dispersionInquirer)
        {
        }
    }
}
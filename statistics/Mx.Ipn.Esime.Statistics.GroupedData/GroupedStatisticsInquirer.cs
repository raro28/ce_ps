namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.GroupedData;

    public class GroupedStatisticsInquirer : StatisticsInquirerBase
    {
        public GroupedStatisticsInquirer(DataContainer dataContainer, DataDistributionFrequencyInquirer frequencyInquirer, GroupedXileInquirer xileInquirer, GroupedCentralTendecyInquirer centralTendecyInquirer, GroupedDispersionInquirer dispersionInquirer) : base(dataContainer, frequencyInquirer, xileInquirer, centralTendecyInquirer, dispersionInquirer)
        {
        }
    }
}
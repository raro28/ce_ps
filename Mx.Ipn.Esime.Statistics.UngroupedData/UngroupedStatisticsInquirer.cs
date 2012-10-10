namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class UngroupedStatisticsInquirer : StatisticsInquirerBase
    {
        public UngroupedStatisticsInquirer(DataContainer dataContainer, UngroupedXileInquirer xileInquirer, UngroupedCentralTendecyInquirer centralTendecyInquirer, UngroupedDispersionInquirer dispersionInquirer) : base(dataContainer, xileInquirer, centralTendecyInquirer, dispersionInquirer)
        {
        }
    }
}
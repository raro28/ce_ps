namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class UngroupedStatisticsInquirer : StatisticsInquirerBase
    {
        static UngroupedStatisticsInquirer()
        {
            Kernel.Bind<UngroupedXileInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<UngroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            Kernel.Bind<UngroupedDispersionInquirer>().ToSelf().InSingletonScope();
        }

        public UngroupedStatisticsInquirer(DataContainer dataContainer, UngroupedXileInquirer xileInquirer, UngroupedCentralTendecyInquirer centralTendecyInquirer, UngroupedDispersionInquirer dispersionInquirer) : base(dataContainer, xileInquirer, centralTendecyInquirer, dispersionInquirer)
        {
        }
    }
}
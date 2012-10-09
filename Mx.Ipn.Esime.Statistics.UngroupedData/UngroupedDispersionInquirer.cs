namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class UngroupedDispersionInquirer : DispersionInquirerBase
    {
        public UngroupedDispersionInquirer(DataContainer dataContainer, UngroupedXileInquirer xileInquirer, UngroupedCentralTendecyInquirer centralTendecyInquirer) : base(dataContainer, xileInquirer, centralTendecyInquirer)
        {		
            this.XileInquirer = xileInquirer;
            this.CentralTendecyInquirer = centralTendecyInquirer;
        }

        protected override double CalcAbsoluteDeviation()
        {
            var nAbsDev = 0.0;
            foreach (var item in DataContainer.Data)
            {
                nAbsDev += Math.Abs(item - this.CentralTendecyInquirer.GetMean());
            }

            var mad = nAbsDev / DataContainer.DataCount;

            return mad;
        }

        protected override double CalcVariance()
        {
            var nplus1Variance = 0.0;
            foreach (var item in DataContainer.Data)
            {
                nplus1Variance += Math.Pow(item - this.CentralTendecyInquirer.GetMean(), 2);
            }

            var variance = nplus1Variance / (DataContainer.DataCount - 1);

            return variance;
        }

        protected override double CalcMomentum(int nMomentum)
        {
            var momentum = 0.0;
            foreach (var item in DataContainer.Data)
            {
                var meanDiff = item - this.CentralTendecyInquirer.GetMean();
                momentum += Math.Pow(meanDiff, nMomentum);
            }

            momentum /= DataContainer.DataCount;
            return momentum;
        }

        protected override double CalcDataRange()
        {
            return DataContainer.Data.Max() - DataContainer.Data.Min();
        }
    }
}
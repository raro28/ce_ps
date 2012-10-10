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

        public override double GetDataRange()
        {
            return DataContainer.Data.Max() - DataContainer.Data.Min();
        }

        public override double GetAbsoluteDeviation()
        {
            var nAbsDev = 0.0;
            foreach (var item in DataContainer.Data)
            {
                nAbsDev += Math.Abs(item - this.CentralTendecyInquirer.GetMean());
            }
            
            var mad = nAbsDev / DataContainer.DataCount;
            
            return mad;
        }

        public override double GetVariance()
        {
            var nplus1Variance = 0.0;
            foreach (var item in DataContainer.Data)
            {
                nplus1Variance += Math.Pow(item - this.CentralTendecyInquirer.GetMean(), 2);
            }
            
            var variance = nplus1Variance / (DataContainer.DataCount - 1);
            
            return variance;
        }

        protected override double GetMomentum(int nMomentum)
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
    }
}
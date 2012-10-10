namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class UngroupedDispersionInquirer : DispersionInquirerBase
    {
        public UngroupedDispersionInquirer(DataContainer dataContainer, UngroupedXileInquirer xileInquirer, UngroupedCentralTendecyInquirer centralTendecyInquirer) : base(dataContainer, xileInquirer, centralTendecyInquirer)
        {		
            this.XileInquirer = xileInquirer;
            this.CentralTendecyInquirer = centralTendecyInquirer;
        }

        public override double GetDataRange()
        {
            var range = DataContainer.Data.Max() - DataContainer.Data.Min();
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.DataRange, range));

            return range;
        }

        public override double GetAbsoluteDeviation()
        {
            var nAbsDev = 0.0;
            foreach (var item in DataContainer.Data)
            {
                nAbsDev += Math.Abs(item - this.CentralTendecyInquirer.GetMean());
            }
            
            var mad = nAbsDev / DataContainer.DataCount;
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.AbsoluteDeviation, mad));

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
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.Variance, variance));

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
            var keyMomentum = string.Format(TaskNames.MomentumFormat, nMomentum);
            this.FireResolvedEvent(this, new InquiryEventArgs(keyMomentum, momentum));

            return momentum;
        }
    }
}
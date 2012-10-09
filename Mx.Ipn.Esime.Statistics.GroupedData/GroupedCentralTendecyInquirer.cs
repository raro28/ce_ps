namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class GroupedCentralTendecyInquirer : CentralTendecyInquirerBase
    {
        public GroupedCentralTendecyInquirer(DataDistributionFrequencyInquirer distributionInquirer, GroupedXileInquirer xileInquirer) : base(distributionInquirer.DataContainer, xileInquirer)
        {           
            this.DistributionInquirer = distributionInquirer;
            this.XileInquirer = xileInquirer;
        }

        private DataDistributionFrequencyInquirer DistributionInquirer
        {
            get;
            set;
        }

        private GroupedXileInquirer XileInquirer
        {
            get;
            set;
        }

        protected override double CalcMean()
        {
            this.DistributionInquirer.AddFrequenciesTimesClassMarks();
            var table = this.DistributionInquirer.GetTable();
            double fxSum = 0;
            foreach (var item in table)
            {
                fxSum += item.fX;
            }

            var mean = fxSum / DataContainer.DataCount;

            return mean;
        }

        protected override double CalcMedian()
        {
            var median = this.XileInquirer.GetQuartile(2);

            return median;
        }

        protected override IList<double> CalcModes()
        {
            this.DistributionInquirer.AddFrequencies();
            this.DistributionInquirer.AddRealClassIntervals();
            var table = this.DistributionInquirer.GetTable().ToList();
            var firstMaxFreqItem = table.OrderByDescending(item => item.Frequency).First();
            var maxFreqItems = table.Where(item => item.Frequency == firstMaxFreqItem.Frequency).ToList();

            var modes = new List<double>();

            foreach (var maxFreqItem in maxFreqItems)
            {               
                var iMaxFreqItem = table.IndexOf(maxFreqItem);
                
                var d1 = maxFreqItem.Frequency - (iMaxFreqItem != 0 ? table[iMaxFreqItem - 1].Frequency : 0);
                var d2 = maxFreqItem.Frequency - (iMaxFreqItem < (table.Count - 1) ? table[iMaxFreqItem + 1].Frequency : 0);
                
                var mode = maxFreqItem.RealInterval.From + ((d1 * this.DistributionInquirer.Amplitude) / (d1 + d2));

                modes.Add(mode);
            }

            return modes;
        }
    }
}
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class GroupedDispersionInquirer : DispersionInquirerBase
    {
        public GroupedDispersionInquirer(DataDistributionFrequencyInquirer distributionInquirer, GroupedXileInquirer xileInquirer, GroupedCentralTendecyInquirer centralTendecyInquirer) : base(distributionInquirer.DataContainer, xileInquirer, centralTendecyInquirer)
        {           
            this.DistributionInquirer = distributionInquirer;
            this.XileInquirer = xileInquirer;
            this.CentralTendecyInquirer = centralTendecyInquirer;
        }

        private DataDistributionFrequencyInquirer DistributionInquirer
        {
            get;
            set;
        }

        public override double GetDataRange()
        {
            var table = this.DistributionInquirer.Table;
            var range = table[0].ClassInterval.To - table[table.Count - 1].ClassInterval.From;
            
            return range;
        }

        public override double GetAbsoluteDeviation()
        {
            var mad = this.MeanDifferenceSum(1) / DataContainer.DataCount;
            
            return mad;
        }

        public override double GetVariance()
        {
            var variance = this.MeanDifferenceSum(2) / (DataContainer.DataCount - 1);
            
            return variance;
        }

        public void AddMeanDifference(int power)
        {       
            if (power < 1 || power > 4)
            {
                throw new StatisticsException(string.Format(ExceptionMessages.Invalid_Power_Format, power));
            }
            
            var keyProperty = string.Format(TaskNames.MeanDiff_Property_Format, power);
            this.DistributionInquirer.AddClassMarks();
            this.DistributionInquirer.AddFrequencies();
            var frequencyTable = this.DistributionInquirer.Table;
            
            var mean = CentralTendecyInquirer.GetMean();
            foreach (var item in frequencyTable)
            {
                var difference = power != 1 ? item.ClassMark - mean : Math.Abs(item.ClassMark - mean);
                ((IDictionary<string, object>)item).Add(keyProperty, item.Frequency * Math.Pow(difference, power));
            }
        }

        protected override double GetMomentum(int nMomentum)
        {
            var momentum = this.MeanDifferenceSum(nMomentum) / DataContainer.DataCount;
            
            return momentum;
        }

        private double MeanDifferenceSum(int power)
        {
            this.AddMeanDifference(power);
            double sum = 0;
            var table = this.DistributionInquirer.Table;
            foreach (var item in table)
            {
                sum += ((IDictionary<string, dynamic>)item)[string.Format(TaskNames.MeanDiff_Property_Format, power)];
            }

            return sum;
        }
    }
}
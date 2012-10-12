namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
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
            var table = this.DistributionInquirer.GetTable();
            var range = table[0].ClassInterval.To - table[table.Count - 1].ClassInterval.From;
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.DataRange, range));

            return range;
        }

        [AnswerAttribute(Name = "MeanDifference_Format", Type = typeof(TaskNames), Formated = true)]
        public void AddMeanDifference(int power)
        {       
            var keyProperty = string.Format(TaskNames.MeanDiff_Property_Format, power);
            this.DistributionInquirer.AddClassMarks();
            this.DistributionInquirer.AddFrequencies();
            var frequencyTable = this.DistributionInquirer.GetTable();
            
            var mean = CentralTendecyInquirer.GetMean();
            foreach (var item in frequencyTable)
            {
                var entry = (IDictionary<string, object>)item;
                var difference = power != 1 ? item.ClassMark - mean : Math.Abs(item.ClassMark - mean);
                var powDifference = item.Frequency * Math.Pow(difference, power);

                if (!entry.ContainsKey(keyProperty))
                {
                    entry.Add(keyProperty, powDifference);
                }
                else
                {
                    entry[keyProperty] = powDifference;
                }
            }

            var keyDifference = string.Format(TaskNames.MeanDifference_Format, power);
            this.FireResolvedEvent(this, new InquiryEventArgs(keyDifference, TaskNames.DispersionTable));
        }

        protected override double MeanDifferenceSum(int power)
        {
            DispersionInquirerBase.AssertValidPower(power);

            this.AddMeanDifference(power);
            double sum = 0;
            var table = this.DistributionInquirer.GetTable();
            foreach (var item in table)
            {
                sum += ((IDictionary<string, dynamic>)item)[string.Format(TaskNames.MeanDiff_Property_Format, power)];
            }

            return sum;
        }
    }
}
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;
	
    public class UngroupedCentralTendecyInquirer : InquirerBase, ICentralTendencyInquirer
    {
        public UngroupedCentralTendecyInquirer(DataContainer dataContainer) : base(dataContainer)
        {	
        }

        [AnswerAttribute(Name = "Mean", Type = typeof(TaskNames))]
        public double GetMean()
        {
            var mean = DataContainer.Data.Sum() / DataContainer.DataCount;
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.Mean, mean));

            return mean;
        }

        [AnswerAttribute(Name = "Median", Type = typeof(TaskNames))]
        public double GetMedian()
        {
            var midIndex = (DataContainer.DataCount / 2) - 1;
            var median = DataContainer.DataCount % 2 != 0 ? DataContainer.Data[midIndex + 1] : (DataContainer.Data[midIndex] + DataContainer.Data[midIndex + 1]) / 2;
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.Median, median));

            return median;
        }

        [AnswerAttribute(Name = "Modes", Type = typeof(TaskNames))]
        public IList<double> GetModes()
        {
            var groups = DataContainer.Data.GroupBy(item => item);
            var modes = (from _mode in groups
                         where _mode.Count() == groups.Max(grouped => grouped.Count())
                         select _mode.First()).ToList();
            this.FireResolvedEvent(this, new InquiryEventArgs(TaskNames.Modes, modes));

            return modes;
        }
    }
}
namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public abstract class CentralTendecyInquirerBase : InquirerBase, ICentralTendencyInquirer
    {
        public CentralTendecyInquirerBase(DataContainer dataContainer) : base(dataContainer)
        {
        }

        public double GetMean()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.Mean))
            {
                DataContainer.Answers.Add(TaskNames.Mean, this.CalcMean());
            }

            return DataContainer.Answers[TaskNames.Mean];
        }
		
        public double GetMedian()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.Median))
            {
                DataContainer.Answers.Add(TaskNames.Median, this.CalcMedian());
            }

            return DataContainer.Answers[TaskNames.Median];
        }
		
        public IList<double> GetModes()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.Modes))
            {
                DataContainer.Answers.Add(TaskNames.Modes, this.CalcModes());
            }

            return DataContainer.Answers[TaskNames.Modes];
        }

        protected abstract double CalcMean();

        protected abstract double CalcMedian();

        protected abstract IList<double> CalcModes();
    }
}
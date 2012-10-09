namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class DataDistributionFrequencyInquirer : InquirerBase
    {
        public DataDistributionFrequencyInquirer(DataContainer dataContainer) : base(dataContainer)
        {
            this.InitProperties();
        }

        public double Max
        {
            get;
            private set;
        }

        public double Min
        {
            get;
            private set;
        }

        public double Range
        {
            get;
            private set;
        }

        public int GroupsCount
        {
            get;
            private set;
        }

        public double Amplitude
        {
            get;
            private set;
        }

        public IEnumerable<dynamic> GetTable()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.DispersionTable))
            {
                this.AddClassIntervals();
            }

            return DataContainer.Answers[TaskNames.DispersionTable];
        }

        public void AddClassIntervals()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.ClassIntervals))
            {
                var frequencyTable = new List<dynamic>(this.GroupsCount);
                DataContainer.Answers.Add(TaskNames.ClassIntervals, TaskNames.DispersionTable);
                DataContainer.Answers.Add(TaskNames.DispersionTable, frequencyTable.AsReadOnly());
                var inferiorClassLimit = this.Min;
                var superiorClassLimit = inferiorClassLimit + this.Amplitude - DataContainer.DataPrecisionValue;
                for (int i = 1; i <= this.GroupsCount; i++)
                {
                    var interval = new Interval(inferiorClassLimit, superiorClassLimit);

                    dynamic distElement = new ExpandoObject();
                    distElement.ClassInterval = interval;
                    frequencyTable.Add(distElement);
                    inferiorClassLimit += this.Amplitude;
                    superiorClassLimit += this.Amplitude;
                }
            }
        }

        public void AddFrequencies()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.Frequencies))
            {
                this.AddClassIntervals();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.Frequencies, TaskNames.DispersionTable);
                foreach (var tableItem in frequencyTable)
                {
                    var frequency = DataContainer.Data.Count(item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
                    tableItem.Frequency = frequency;
                }
            }
        }

        public void AddAcumulatedFrequencies()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.AcumulatedFrequencies))
            {
                this.AddFrequencies();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.AcumulatedFrequencies, TaskNames.DispersionTable);
                var lastFrequency = 0;
                foreach (var item in frequencyTable)
                {
                    item.AcumulatedFrequency = item.Frequency + lastFrequency;
                    lastFrequency = item.AcumulatedFrequency;
                }
            }
        }

        public void AddRelativeFrequencies()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.RelativeFrequencies))
            {
                this.AddFrequencies();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.RelativeFrequencies, TaskNames.DispersionTable);
                foreach (var item in frequencyTable)
                {
                    item.RelativeFrequency = (double)item.Frequency / DataContainer.DataCount;
                }
            }
        }

        public void AddAcumulatedRelativeFrequencies()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.AcumulatedRelativeFrequencies))
            {
                this.AddRelativeFrequencies();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.AcumulatedRelativeFrequencies, TaskNames.DispersionTable);
                var lastRelativeFrequency = 0.0;
                foreach (var item in frequencyTable)
                {
                    item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
                    lastRelativeFrequency = item.AcumulatedRelativeFrequency;
                }
            }
        }

        public void AddClassMarks()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.ClassMarks))
            {
                this.AddClassIntervals();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.ClassMarks, TaskNames.DispersionTable);
                foreach (var item in frequencyTable)
                {
                    var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
                    item.ClassMark = classMark;
                }
            }
        }

        public void AddRealClassIntervals()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.RealClassIntervals))
            {
                this.AddClassIntervals();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.RealClassIntervals, TaskNames.DispersionTable);
                var midPrecision = DataContainer.DataPrecisionValue / 2;
                foreach (var item in frequencyTable)
                {
                    var realInterval = new Interval(item.ClassInterval.From - midPrecision, item.ClassInterval.To + midPrecision);

                    item.RealInterval = realInterval;
                }
            }
        }

        public void AddFrequenciesTimesClassMarks()
        {
            if (!DataContainer.Answers.ContainsKey(TaskNames.FrequenciesTimesClassMarks))
            {
                this.AddFrequencies();
                this.AddClassMarks();
                var frequencyTable = this.GetTable();
                DataContainer.Answers.Add(TaskNames.FrequenciesTimesClassMarks, TaskNames.DispersionTable);
                foreach (var item in frequencyTable)
                {
                    item.fX = item.Frequency * item.ClassMark;
                }
            }
        }

        private void InitProperties()
        {
            this.Max = DataContainer.Data.Max();
            this.Min = DataContainer.Data.Min();

            this.Range = this.Max - this.Min;

            this.GroupsCount = (int)Math.Round(Math.Sqrt(this.DataContainer.DataCount));

            this.Amplitude = Math.Round(this.Range / this.GroupsCount, this.DataContainer.DataPrecision);

            if ((this.Min + (this.Amplitude * this.GroupsCount) - this.DataContainer.DataPrecisionValue) <= this.Max)
            {
                this.Amplitude += this.DataContainer.DataPrecisionValue;
            }
        }
    }   
}
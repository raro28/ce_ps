namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class DataDistributionFrequencyInquirer : InquirerBase
    {
        public DataDistributionFrequencyInquirer(DataContainer dataContainer) : base(dataContainer)
        {
            this.InitProperties();
            this.InitTable();
        }

        public ReadOnlyCollection<dynamic> Table
        {
            get;
            private set;
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

        public void AddFrequencies()
        {            
            var frequencyTable = this.Table;
            foreach (var tableItem in frequencyTable)
            {
                var frequency = DataContainer.Data.Count(item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
                tableItem.Frequency = frequency;
            }
        }

        public void AddAcumulatedFrequencies()
        {
            this.AddFrequencies();
            var frequencyTable = this.Table;
            var lastFrequency = 0;
            foreach (var item in frequencyTable)
            {
                item.AcumulatedFrequency = item.Frequency + lastFrequency;
                lastFrequency = item.AcumulatedFrequency;
            }
        }

        public void AddRelativeFrequencies()
        {           
            this.AddFrequencies();
            var frequencyTable = this.Table;
            foreach (var item in frequencyTable)
            {
                item.RelativeFrequency = (double)item.Frequency / DataContainer.DataCount;
            }
        }

        public void AddAcumulatedRelativeFrequencies()
        {
            this.AddRelativeFrequencies();
            var frequencyTable = this.Table;
            var lastRelativeFrequency = 0.0;
            foreach (var item in frequencyTable)
            {
                item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
                lastRelativeFrequency = item.AcumulatedRelativeFrequency;
            }
        }

        public void AddClassMarks()
        {                       
            var frequencyTable = this.Table;
            foreach (var item in frequencyTable)
            {
                var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
                item.ClassMark = classMark;
            }
        }

        public void AddRealClassIntervals()
        {            
            var frequencyTable = this.Table;
            var midPrecision = DataContainer.DataPrecisionValue / 2;
            foreach (var item in frequencyTable)
            {
                var realInterval = new Interval(item.ClassInterval.From - midPrecision, item.ClassInterval.To + midPrecision);

                item.RealInterval = realInterval;
            }
        }

        public void AddFrequenciesTimesClassMarks()
        {
            this.AddFrequencies();
            this.AddClassMarks();
            var frequencyTable = this.Table;
            foreach (var item in frequencyTable)
            {
                item.fX = item.Frequency * item.ClassMark;
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

        private void InitTable()
        {
            var table = new List<dynamic>(this.GroupsCount);
            var inferiorClassLimit = this.Min;
            var superiorClassLimit = inferiorClassLimit + this.Amplitude - DataContainer.DataPrecisionValue;
            for (int i = 1; i <= this.GroupsCount; i++)
            {
                var interval = new Interval(inferiorClassLimit, superiorClassLimit);
                
                dynamic distElement = new ExpandoObject();
                distElement.ClassInterval = interval;
                table.Add(distElement);
                inferiorClassLimit += this.Amplitude;
                superiorClassLimit += this.Amplitude;
            }
            
            this.Table = table.AsReadOnly();
        }
    }   
}
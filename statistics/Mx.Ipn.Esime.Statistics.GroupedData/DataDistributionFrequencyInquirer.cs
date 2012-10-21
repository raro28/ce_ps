/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class DataDistributionFrequencyInquirer : InquirerBase
    {
        public DataDistributionFrequencyInquirer(DataContainer dataContainer) : base(dataContainer)
        {
            this.InitProperties();
            this.InitTable();
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

        private ReadOnlyCollection<dynamic> Table
        {
            get;
            set;
        }

        public IList<dynamic> GetTable()
        {
            return this.Table;
        }

        public void AddFrequencies()
        {      
            Action action = () =>
            {
                var frequencyTable = this.Table;
                foreach (var tableItem in frequencyTable)
                {
                    var frequency = Container.Data.Count(item => item >= tableItem.ClassInterval.From && item <= tableItem.ClassInterval.To);
                    tableItem.Frequency = frequency;
                }
            };

            this.Container.Register(TaskNames.Frequencies, TaskNames.DispersionTable, action);
        }

        public void AddAcumulatedFrequencies()
        {
            Action action = () =>
            {
                this.AddFrequencies();
                var frequencyTable = this.Table;
                var lastFrequency = 0;
                foreach (var item in frequencyTable)
                {
                    item.AcumulatedFrequency = item.Frequency + lastFrequency;
                    lastFrequency = item.AcumulatedFrequency;
                }
            };

            this.Container.Register(TaskNames.AcumulatedFrequencies, TaskNames.DispersionTable, action);
        }

        public void AddRelativeFrequencies()
        {    
            Action action = () =>
            {
                this.AddFrequencies();
                var frequencyTable = this.Table;
                foreach (var item in frequencyTable)
                {
                    item.RelativeFrequency = (double)item.Frequency / Container.DataCount;
                }
            };

            this.Container.Register(TaskNames.RelativeFrequencies, TaskNames.DispersionTable, action);
        }

        public void AddAcumulatedRelativeFrequencies()
        {
            Action action = () =>
            {
                this.AddRelativeFrequencies();
                var frequencyTable = this.Table;
                var lastRelativeFrequency = 0.0;
                foreach (var item in frequencyTable)
                {
                    item.AcumulatedRelativeFrequency = item.RelativeFrequency + lastRelativeFrequency;
                    lastRelativeFrequency = item.AcumulatedRelativeFrequency;
                }
            };

            this.Container.Register(TaskNames.AcumulatedRelativeFrequencies, TaskNames.DispersionTable, action);
        }

        public void AddClassMarks()
        {   
            Action action = () =>
            {
                var frequencyTable = this.Table;
                foreach (var item in frequencyTable)
                {
                    var classMark = (item.ClassInterval.From + item.ClassInterval.To) / 2;
                    item.ClassMark = Math.Round(classMark, this.Container.DataPrecision + 1, MidpointRounding.AwayFromZero);
                }
            };

            this.Container.Register(TaskNames.ClassMarks, TaskNames.DispersionTable, action);
        }

        public void AddRealClassIntervals()
        {   
            Action action = () =>
            {
                var frequencyTable = this.Table;
                var midPrecision = Container.DataPrecisionValue / 2;
                foreach (var item in frequencyTable)
                {
                    var realInterval = new Interval(item.ClassInterval.From - midPrecision, item.ClassInterval.To + midPrecision);

                    item.RealInterval = realInterval;
                }
            };

            this.Container.Register(TaskNames.RealClassIntervals, TaskNames.DispersionTable, action);
        }

        public void AddFrequenciesTimesClassMarks()
        {
            Action action = () =>
            {
                this.AddFrequencies();
                this.AddClassMarks();
                var frequencyTable = this.Table;
                foreach (var item in frequencyTable)
                {
                    item.fX = item.Frequency * item.ClassMark;
                }
            };

            this.Container.Register(TaskNames.FrequenciesTimesClassMarks, TaskNames.DispersionTable, action);
        }

        private void InitProperties()
        {
            this.Range = this.Container.Max - this.Container.Min;

            this.GroupsCount = (int)Math.Round(Math.Sqrt(this.Container.DataCount), MidpointRounding.AwayFromZero);

            this.Amplitude = Math.Round(this.Range / this.GroupsCount, this.Container.DataPrecision, MidpointRounding.AwayFromZero);

            if ((this.Container.Min + (this.Amplitude * this.GroupsCount) - this.Container.DataPrecisionValue) <= this.Container.Max)
            {
                this.Amplitude += this.Container.DataPrecisionValue;
            }
        }

        private void InitTable()
        {
            var table = new List<dynamic>(this.GroupsCount);
            var inferiorClassLimit = this.Container.Min;
            var superiorClassLimit = inferiorClassLimit + this.Amplitude - Container.DataPrecisionValue;
            for (int i = 1; i <= this.GroupsCount; i++)
            {
                var interval = new Interval(Math.Round(inferiorClassLimit, this.Container.DataPrecision), Math.Round(superiorClassLimit, this.Container.DataPrecision));
                
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
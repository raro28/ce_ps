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
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class GroupedCentralTendecyInquirer : InquirerBase, ICentralTendencyInquirer
    {
        public GroupedCentralTendecyInquirer(DataDistributionFrequencyInquirer distributionInquirer, GroupedXileInquirer xileInquirer) : base(distributionInquirer.Container, xileInquirer)
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

        public double GetMean()
        {
            Func<double> func = () =>
            {
                this.DistributionInquirer.AddFrequenciesTimesClassMarks();
                var table = this.DistributionInquirer.GetTable();
                double fxSum = 0;
                foreach (var item in table)
                {
                    fxSum += item.fX;
                }
                
                var mean = fxSum / Container.DataCount;

                return mean;
            };

            return this.Container.Register(TaskNames.Mean, func);
        }

        public double GetMedian()
        {
            Func<double> func = () => this.XileInquirer.GetQuartile(2);

            return this.Container.Register(TaskNames.Median, func);
        }

        public IList<double> GetModes()
        {
            Func<IList<double>> func = () =>
            {
                this.DistributionInquirer.AddFrequencies();
                this.DistributionInquirer.AddRealClassIntervals();
                var table = this.DistributionInquirer.GetTable();
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
            };

            return this.Container.Register(TaskNames.Modes, func);
        }
    }
}
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
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class GroupedDispersionInquirer : DispersionInquirerBase
    {
        public GroupedDispersionInquirer(DataDistributionFrequencyInquirer distributionInquirer, GroupedXileInquirer xileInquirer, GroupedCentralTendecyInquirer centralTendecyInquirer) : base(distributionInquirer.Container, xileInquirer, centralTendecyInquirer)
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
            Func<double> func = () =>
            {
                var table = this.DistributionInquirer.GetTable();
                var range = table[table.Count - 1].ClassInterval.From - table[0].ClassInterval.To;

                return range;
            };

            return this.Container.Register(TaskNames.DataRange, func);
        }

        public void AddMeanDifference(int power)
        {   
            Action action = () =>
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
            };

            var keyDifference = string.Format(TaskNames.MeanDifference_Format, power);

            this.Container.Register(keyDifference, TaskNames.DispersionTable, action);
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
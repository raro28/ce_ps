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
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class GroupedXileInquirer : XileInquirerBase
    {
        public GroupedXileInquirer(DataDistributionFrequencyInquirer distributionInquirer) : base(distributionInquirer.Container)
        {           
            this.DistributionInquirer = distributionInquirer;
        }

        private DataDistributionFrequencyInquirer DistributionInquirer
        {
            get;
            set;
        }

        protected override double CalcXile(double lx)
        {
            this.DistributionInquirer.AddFrequencies();
            this.DistributionInquirer.AddAcumulatedFrequencies();
            this.DistributionInquirer.AddRealClassIntervals();
            var table = this.DistributionInquirer.GetTable().Skip(1);
            dynamic prevElement = null;
            dynamic targetElement = null;
            foreach (var item in table)
            {
                targetElement = item;
                if (targetElement.AcumulatedFrequency >= lx)
                {
                    break;
                }
                
                prevElement = item;
            }
            
            double prevF = prevElement != null ? prevElement.AcumulatedFrequency : 0;
            var xileResult = targetElement.RealInterval.From + (((lx - prevF) / targetElement.Frequency) * this.DistributionInquirer.Amplitude);
            
            return xileResult;
        }
    }
}
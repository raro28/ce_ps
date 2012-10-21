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
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using System;
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

        public double GetMean()
        {
            Func<double> func = () => Container.Data.Sum() / Container.DataCount;

            return this.Container.Register(TaskNames.Mean, func);
        }

        public double GetMedian()
        {
            Func<double> func = () =>
            {
                var midIndex = (Container.DataCount / 2) - 1;
                var median = Container.DataCount % 2 != 0 ? Container.Data[midIndex + 1] : (Container.Data[midIndex] + Container.Data[midIndex + 1]) / 2;

                return median;
            };
          
            return this.Container.Register(TaskNames.Median, func);
        }

        public IList<double> GetModes()
        {
            Func<IList<double>> func = () =>
            {
                var groups = Container.Data.GroupBy(item => item);
                var modes = (from _mode in groups
                             where _mode.Count() == groups.Max(grouped => grouped.Count())
                             select _mode.First()).ToList();

                return modes;
            };

            return this.Container.Register(TaskNames.Modes, func);
        }
    }
}
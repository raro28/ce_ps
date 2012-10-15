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
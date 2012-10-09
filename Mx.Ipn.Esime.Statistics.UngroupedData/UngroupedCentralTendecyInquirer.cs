namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;
	
    public class UngroupedCentralTendecyInquirer : CentralTendecyInquirerBase
    {
        public UngroupedCentralTendecyInquirer(DataContainer dataContainer) : base(dataContainer)
        {	
        }

        protected override double CalcMean()
        {
            var mean = DataContainer.Data.Sum() / DataContainer.DataCount;

            return mean;
        }

        protected override double CalcMedian()
        {
            var midIndex = (DataContainer.DataCount / 2) - 1;
            var median = DataContainer.DataCount % 2 != 0 ? DataContainer.Data[midIndex + 1] : (DataContainer.Data[midIndex] + DataContainer.Data[midIndex + 1]) / 2;

            return median;
        }

        protected override IList<double> CalcModes()
        {
            var groups = DataContainer.Data.GroupBy(item => item);
            var modes = (from _mode in groups
					where _mode.Count() == groups.Max(grouped => grouped.Count())
						select _mode.First()).ToList();

            return modes;
        }
    }
}
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System.Collections.Generic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class GroupedXileInquirer : XileInquirerBase
    {
        public GroupedXileInquirer(DataContainer dataContainer) : base(dataContainer)
        {           
            this.DistributionInquirer = new DataDistributionFrequencyInquirer(dataContainer);
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
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class UngroupedDispersionInquirer : DispersionInquirerBase
    {
        public UngroupedDispersionInquirer(DataContainer dataContainer, UngroupedXileInquirer xileInquirer, UngroupedCentralTendecyInquirer centralTendecyInquirer) : base(dataContainer, xileInquirer, centralTendecyInquirer)
        {		
            this.XileInquirer = xileInquirer;
            this.CentralTendecyInquirer = centralTendecyInquirer;
        }

        public override double GetDataRange()
        {
            Func<double> func = () => Container.Data.Max() - Container.Data.Min();

            return this.Container.Register(TaskNames.DataRange, func);
        }

        protected override double MeanDifferenceSum(int power)
        {
            DispersionInquirerBase.AssertValidPower(power);

            var mean = this.CentralTendecyInquirer.GetMean();
            double sum = 0;
            foreach (var item in Container.Data)
            {
                var difference = power != 1 ? item - mean : Math.Abs(item - mean);
                var powDifference = Math.Pow(difference, power);

                sum += powDifference;
            }

            return sum;
        }
    }
}
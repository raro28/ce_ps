namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;

    public class UngroupedHelperMethods:HelperMethods
    {
        public UngroupedHelperMethods()
        {
            this.CalcNthXile = (data, xile, nTh) => {
                var lx = data.Count * nTh / (double)xile;
                var li = (int)Math.Floor(lx - 0.5);
                var ls = (int)Math.Floor(lx + 0.5);
                if (ls == data.Count)
                {
                    ls = li;
                }
                var iPortion = li + 1 - (lx - 0.5);
                var sPortion = 1 - iPortion;
                var xRange = iPortion * data[li] + sPortion * data[ls];
                
                return xRange;
            };

            this.SampleMean = data => {
                var sum = 0.0;
                data.ForEach(item => sum += item);
                var mean = sum / data.Count;
                
                return mean;
            };
        }

        public override TInquirer NewInquirer<TInquirer>(Ninject.StandardKernel kernel)
        {
            kernel
                .Bind<UngroupedXileInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<UngroupedCentralTendecyInquirer>()
                    .ToSelf().InSingletonScope();
            kernel
                .Bind<UngroupedDispersionInquirer>()
                    .ToSelf().InSingletonScope();
            
            return kernel.Get<TInquirer>();
        }
    }
}
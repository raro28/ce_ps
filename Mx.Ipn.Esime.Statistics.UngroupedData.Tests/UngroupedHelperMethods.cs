namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
    using System.Collections.Generic;
    using Mx.Ipn.Esime.Statistics.BaseData.Tests;
    using Ninject;

    public class UngroupedHelperMethods:HelperMethodsBase
    {
        static UngroupedHelperMethods()
        {
        }

        public override double CalcNthXile(IList<double> data, int xile, int nTh)
        {
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
        }
		
        public override double SampleMean(List<double> data)
        {			
            var sum = 0.0;
            data.ForEach(item => sum += item);
            var mean = sum / data.Count;
			
            return mean;
        }
    }
}
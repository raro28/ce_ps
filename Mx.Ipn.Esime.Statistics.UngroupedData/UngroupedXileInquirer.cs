namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public class UngroupedXileInquirer : XileInquirerBase
    {
        public UngroupedXileInquirer(DataContainer dataContainer) : base(dataContainer)
        {			
        }

        protected override double CalcXile(double lx)
        {
            var li = (int)Math.Floor(lx - 0.5);
            var ls = (int)Math.Floor(lx + 0.5);
            if (ls == DataContainer.DataCount)
            {
                ls = li;
            }
			
            var iPortion = li + 1 - (lx - 0.5);
            var sPortion = 1 - iPortion;
			
            var xileResult = (iPortion * DataContainer.Data[li]) + (sPortion * DataContainer.Data[ls]);
            return xileResult;
        }
    }
}
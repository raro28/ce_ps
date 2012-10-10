namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public enum Xiles
    {
        Quartile = 4,
        Decile = 10,
        Percentile = 100
    }

    public abstract class XileInquirerBase : InquirerBase, IXileInquirer
    {       
        public XileInquirerBase(DataContainer dataContainer, params InquirerBase[] dependencies) : base(dataContainer, dependencies)
        {           
        }

        public double GetDecile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Decile);
            var nThDecile = this.GetXile(xileInfo);
            
            return nThDecile;
        }
        
        public double GetPercentile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Percentile);
            var nThPercentile = this.GetXile(xileInfo);
            
            return nThPercentile;
        }
        
        public double GetQuartile(int nTh)
        {
            var xileInfo = AssertValidXile(nTh, Xiles.Quartile);
            var nThQuartile = this.GetXile(xileInfo);
            
            return nThQuartile;
        }

        protected abstract double CalcXile(double lx);
            
        private static XileInfo AssertValidXile(int nTh, Xiles xile)
        {
            if (nTh < 1 || nTh > (int)xile)
            {
                var xileName = Enum.GetName(typeof(Xiles), xile);
                throw new StatisticsException(string.Format(ExceptionMessages.Invalid_Xile_Name_Format, xileName), new IndexOutOfRangeException(string.Format(ExceptionMessages.Invalid_Xile_Name_Number_Format, xileName, nTh)));
            }
            
            return new XileInfo(xile, nTh);
        }

        private double GetXile(XileInfo xileInfo)
        {
            double xileResult;
            var lx = DataContainer.DataCount * xileInfo.NthXile / (double)xileInfo.Xile;
            xileResult = this.CalcXile(lx);
            
            return xileResult;
        }

        internal class XileInfo
        {
            public readonly Xiles Xile;
            public readonly int NthXile;
            
            public XileInfo(Xiles xile, int nthXile)
            {
                this.Xile = xile;
                this.NthXile = nthXile;
            }
            
            public override string ToString()
            {
                return string.Format(TaskNames.XileFormat, this.Xile, this.NthXile);
            }
        }
    }
}
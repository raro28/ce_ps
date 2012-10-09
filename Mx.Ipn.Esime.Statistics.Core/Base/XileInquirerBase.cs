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
        public XileInquirerBase(DataContainer dataContainer) : base(dataContainer)
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
            
            return new XileInfo
            {
                Xile = xile, NthXile = nTh
            };
        }

        private double GetXile(XileInfo xileInfo)
        {
            double xileResult;
            if (!DataContainer.Answers.ContainsKey(xileInfo.ToString()))
            {
                var lx = DataContainer.DataCount * xileInfo.NthXile / (double)xileInfo.Xile;
                xileResult = this.CalcXile(lx);

                DataContainer.Answers.Add(xileInfo.ToString(), xileResult);
            }
            else
            {
                xileResult = DataContainer.Answers[xileInfo.ToString()];
            }
            
            return xileResult;
        }
    }
}
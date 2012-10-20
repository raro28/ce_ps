namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class XileInfo
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
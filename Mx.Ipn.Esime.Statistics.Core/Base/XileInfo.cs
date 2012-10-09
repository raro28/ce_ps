namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class XileInfo
    {
        public Xiles Xile
        {
            get;
            set;
        }
        
        public int NthXile
        {
            get;
            set;
        }

        public override String ToString()
        {
            return string.Format(TaskNames.XileFormat, this.Xile, this.NthXile);
        }
    }
}
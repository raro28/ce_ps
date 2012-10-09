namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using Mx.Ipn.Esime.Statistics.Core;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class Interval
    {
        public double From
        {
            get;
            set;
        }
		
        public double To
        {
            get;
            set;
        }
		
        public override String ToString()
        {
            return string.Format(TaskNames.Interval_Format, this.From, this.To);
        }
    }		
}
namespace Mx.Ipn.Esime.Statistics.GroupedData
{
    using Mx.Ipn.Esime.Statistics.Core.Resources;

    public class Interval
    {
        public readonly double From;
        public readonly double To;
        
        public Interval(double fromValue, double toValue)
        {
            this.From = fromValue;
            this.To = toValue;
        }
        
        public override string ToString()
        {
            return string.Format(TaskNames.Interval_Format, this.From, this.To);
        }
    }		
}
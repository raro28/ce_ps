namespace Mx.Ipn.Esime.Statistics.Core
{
    using System.Collections.Generic;

    public interface ICentralTendencyInquirer : IInquirer
    {
        double GetMean();
        
        double GetMedian();
        
        IList<double> GetModes();
    }   
}
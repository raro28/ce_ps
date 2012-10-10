namespace Mx.Ipn.Esime.Statistics.Core
{
    public interface IDispersionInquirer : IInquirer
    {
        double GetDataRange();
        
        double GetInterQuartileRange();
        
        double GetInterDecileRange();
        
        double GetInterPercentileRange();

        double GetAbsoluteDeviation();
        
        double GetVariance();
        
        double GetStandarDeviation();
        
        double GetCoefficientOfVariation();
        
        double GetCoefficientOfSymmetry();
        
        double GetCoefficientOfKourtosis();
    }   
}
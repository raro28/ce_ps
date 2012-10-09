namespace Mx.Ipn.Esime.Statistics.Core
{
    public interface IDataDistributionFrequencyInquirer
    {
        void AddClassIntervals();
        
        void AddFrequencies();
        
        void AddAcumulatedFrequencies();
        
        void AddRelativeFrequencies();
        
        void AddAcumulatedRelativeFrequencies();
        
        void AddClassMarks();
        
        void AddRealClassIntervals();
        
        void AddFrequenciesTimesClassMarks();
    }
}
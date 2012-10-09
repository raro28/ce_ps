namespace Mx.Ipn.Esime.Statistics.Core
{   
    public interface IInquirer
    {
        object Inquire(string inquiry, object[] args);
    }
}
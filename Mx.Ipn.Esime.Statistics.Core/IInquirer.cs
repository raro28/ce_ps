namespace Mx.Ipn.Esime.Statistics.Core
{   
    using Mx.Ipn.Esime.Statistics.Core.Base;

    public interface IInquirer
    {
        DataContainer Container
        {
            get;
        }

        bool Inquire(string inquiry, object[] args, out object result);
    }
}
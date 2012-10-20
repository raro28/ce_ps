namespace Mx.Ipn.Esime.Statistics.Core
{
    using System;

    public class StatisticsException : Exception
    {
        public StatisticsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public StatisticsException(string message) : base(message)
        {
        }
    }
}
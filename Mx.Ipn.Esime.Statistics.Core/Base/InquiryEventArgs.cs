namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;

    public class InquiryEventArgs : EventArgs
    {
        public readonly string Inquiry;
        public readonly object Result;

        public InquiryEventArgs(string inquiry, object result)
        {
            this.Inquiry = inquiry;
            this.Result = result;
        }
    }
}
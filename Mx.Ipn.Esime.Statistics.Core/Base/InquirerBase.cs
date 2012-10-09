namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;

    public abstract class InquirerBase
    {
        public readonly DataContainer DataContainer;

        public InquirerBase(DataContainer dataContainer)
        {
            if (dataContainer == null)
            {
                throw new ArgumentNullException("container");
            }

            this.DataContainer = dataContainer;
        }
    }
}
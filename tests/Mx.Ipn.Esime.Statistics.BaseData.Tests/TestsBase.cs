namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
    using System;

    public abstract class TestsBase<THelper> where THelper : HelperMethods
    {
        protected readonly THelper Helper;
        public TestsBase()
        {
            Helper = (THelper)Activator.CreateInstance(typeof(THelper), new object[]{});
        }
    }
}
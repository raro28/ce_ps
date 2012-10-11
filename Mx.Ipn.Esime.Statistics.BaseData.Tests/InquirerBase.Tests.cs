namespace Mx.Ipn.Esime.Statistics.BaseData.Tests
{
	using System;
    using NUnit.Framework;
    using Mx.Ipn.Esime.Statistics.Core;
	
    [TestFixture()]
    public abstract class InquirerBase_Tests<TInquirer,THelper>:TestsBase<THelper> where TInquirer : IInquirer where THelper:HelperMethods
    {
        protected void HandleExceptionThroughTargetInvocationExceptionException(Exception exception)
        {
            Assert.IsInstanceOf<StatisticsException>(exception.InnerException);
        }
    }
}
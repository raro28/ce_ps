using System;
using NUnit.Framework;
using Mx.Ipn.Esime.Statistics.BaseData.Tests;
using Mx.Ipn.Esime.Statistics.Core.Base;

namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
    [TestFixture()]
    public class sdsd:InquirerBase_Tests<GroupedStatisticsInquirer,GroupedHelperMethods>
    {
        [Test()]
        public void TestCase()
        {
            dynamic inquirer = GroupedStatisticsInquirer.CreateInstance<GroupedStatisticsInquirer>(Helper.GetRandomDataSample(100));
            var cok = inquirer.GetCoefficientOfKourtosis();
        }
    }
}


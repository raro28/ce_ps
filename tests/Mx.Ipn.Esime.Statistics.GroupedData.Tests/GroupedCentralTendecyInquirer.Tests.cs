namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedCentralTendecyInquirer_Tests:CentralTendecyInquirerBase_Tests<GroupedCentralTendecyInquirer,GroupedHelperMethods>
	{
		protected override double SampleMedian (IList<double> data)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMedian
			return -1;
		}

		protected override List<double> SampleMode (IEnumerable<double> data)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMode
			return new List<double> ();
		}
	}
}
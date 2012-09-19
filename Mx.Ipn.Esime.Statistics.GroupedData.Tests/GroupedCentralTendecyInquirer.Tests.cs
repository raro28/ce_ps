namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedCentralTendecyInquirer_Tests:CentralTendecyInquirerBase_Tests<GroupedCentralTendecyInquirer>
	{
		public GroupedCentralTendecyInquirer_Tests ():base(()=>{return new GroupedCentralTendecyInquirer (rawData: null);}, new GroupedHelperMethods<GroupedCentralTendecyInquirer> ())
		{
		}

		protected override double SampleMedian (IList<double> sortedData)
		{
			throw new NotImplementedException ();
		}

		protected override List<double> SampleMode (IEnumerable<double> sortedData)
		{
			throw new NotImplementedException ();
		}
	}
}
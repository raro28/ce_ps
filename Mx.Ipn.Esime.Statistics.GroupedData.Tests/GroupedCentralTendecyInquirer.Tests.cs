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
		public GroupedCentralTendecyInquirer_Tests ():base(()=>{return new GroupedCentralTendecyInquirer (rawData: null);})
		{
		}

		protected override double SampleMedian (IList<double> sortedData)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMedian
			throw new NotImplementedException ();
		}

		protected override List<double> SampleMode (IEnumerable<double> sortedData)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMode
			throw new NotImplementedException ();
		}
	}
}
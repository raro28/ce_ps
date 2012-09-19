namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedRangesInquirer_Tests:RangesInquirerBase_Tests<GroupedRangesInquirer>
	{
		public GroupedRangesInquirer_Tests ():base(()=>{return new GroupedRangesInquirer (rawData: null);}, new GroupedHelperMethods<GroupedRangesInquirer> ())
		{
		}

		protected override double SampleDataRange (IList<double> sortedData)
		{
			//TODO:GroupedRangesInquirer_Tests:SampleDataRange
			throw new NotImplementedException ();
		}
	}
}
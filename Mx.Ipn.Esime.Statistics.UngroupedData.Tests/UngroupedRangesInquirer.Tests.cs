namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class UngroupedRangesInquirer_Tests:RangesInquirerBase_Tests<UngroupedRangesInquirer>
	{
		public UngroupedRangesInquirer_Tests ():base(()=>{return new UngroupedRangesInquirer (rawData: null);}, new UngroupedHelperMethods<UngroupedRangesInquirer> ())
		{
		}

		protected override double SampleDataRange (IList<double> sortedData)
		{
			var dataRange = sortedData [sortedData.Count - 1] - sortedData [0];
			
			return dataRange;
		}
	}
}
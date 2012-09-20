namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class UngroupedRangesInquirer_Tests:RangesInquirerBase_Tests<UngroupedRangesInquirer,UngroupedHelperMethods>
	{
		protected override double SampleDataRange (IList<double> data)
		{
			var dataRange = data [data.Count - 1] - data [0];
			
			return dataRange;
		}
	}
}
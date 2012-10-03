namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedDispersionInquirer_Tests:DispersionInquirerBase_Tests<GroupedDispersionInquirer,GroupedHelperMethods>
	{
		protected override double SampleAbsoluteDeviation (List<double> data)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleAbsoluteDeviation
			return -1;
		}

		protected override double SampleVariance (List<double> data)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleVariance
			return -1;
		}

		protected override double SampleMomentum (List<double> data, int nMomentum)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleMomentum
			return -1;
		}

		protected override double SampleDataRange (IList<double> data)
		{
			//TODO:GroupedRangesInquirer_Tests:SampleDataRange
			return -1;
		}
	}
}
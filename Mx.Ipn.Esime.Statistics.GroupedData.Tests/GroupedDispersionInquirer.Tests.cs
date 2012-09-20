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
		protected override double SampleAbsoluteDeviation (List<double> data, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleAbsoluteDeviation
			throw new NotImplementedException ();
		}

		protected override double SampleVariance (List<double> data, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleVariance
			throw new NotImplementedException ();
		}

		protected override double SampleMomentum (List<double> data, int nMomentum, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleMomentum
			throw new NotImplementedException ();
		}
	}
}
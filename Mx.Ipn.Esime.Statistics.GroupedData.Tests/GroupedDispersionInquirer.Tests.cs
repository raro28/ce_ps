namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class GroupedDispersionInquirer_Tests:DispersionInquirerBase_Tests<GroupedDispersionInquirer>
	{
		public GroupedDispersionInquirer_Tests ():base(()=>{return new GroupedDispersionInquirer (rawData: null);}, new GroupedHelperMethods<GroupedDispersionInquirer> ())
		{
		}

		protected override double SampleAbsoluteDeviation (List<double> sortedData, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleAbsoluteDeviation
			throw new NotImplementedException ();
		}

		protected override double SampleVariance (List<double> sortedData, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleVariance
			throw new NotImplementedException ();
		}

		protected override double SampleMomentum (List<double> sortedData, int nMomentum, double mean)
		{
			//TODO:GroupedDispersionInquirer_Tests:SampleMomentum
			throw new NotImplementedException ();
		}
	}
}
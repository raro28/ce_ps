namespace Mx.Ipn.Esime.Statistics.UngroupedData.Tests
{
	using System;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

	[TestFixture()]
	public class UngroupedDispersionInquirer_Tests:DispersionInquirerBase_Tests<UngroupedDispersionInquirer,UngroupedHelperMethods>
	{
		protected override double SampleAbsoluteDeviation (List<double> sortedData, double mean)
		{
			var nAbsoluteDeviation = 0.0;
			sortedData.ForEach (item => nAbsoluteDeviation += Math.Abs (item - mean));
			var absoluteDeviation = nAbsoluteDeviation / sortedData.Count;
			
			return absoluteDeviation;
		}
		
		protected override double SampleVariance (List<double> sortedData, double mean)
		{
			var nplus1Variance = 0.0;
			sortedData.ForEach (item => nplus1Variance += Math.Pow ((item - mean), 2));
			var variance = nplus1Variance / (sortedData.Count - 1);
			
			return variance;
		}
		
		protected override double SampleMomentum (List<double> sortedData, int nMomentum, double mean)
		{
			var sum = 0.0;
			sortedData.ForEach (item => sum += Math.Pow ((item - mean), nMomentum));
			
			var momentum = sum / sortedData.Count;
			
			return momentum;
		}
	}
}
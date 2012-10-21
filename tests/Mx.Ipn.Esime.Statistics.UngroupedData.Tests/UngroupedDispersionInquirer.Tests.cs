/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
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
		protected override double SampleAbsoluteDeviation (List<double> data)
		{
			var nAbsoluteDeviation = 0.0;
			var mean = Helper.SampleMean (data);
			data.ForEach (item => nAbsoluteDeviation += Math.Abs (item - mean));
			var absoluteDeviation = nAbsoluteDeviation / data.Count;
			
			return absoluteDeviation;
		}
		
		protected override double SampleVariance (List<double> data)
		{
			var nplus1Variance = 0.0;
			data.ForEach (item => nplus1Variance += Math.Pow ((item - Helper.SampleMean (data)), 2));
			var variance = nplus1Variance / (data.Count - 1);
			
			return variance;
		}
		
		protected override double SampleMomentum (List<double> data, int nMomentum)
		{
			var sum = 0.0;
			data.ForEach (item => sum += Math.Pow ((item - Helper.SampleMean (data)), nMomentum));
			
			var momentum = sum / data.Count;
			
			return momentum;
		}

		protected override double SampleDataRange (IList<double> data)
		{
			var dataRange = data [data.Count - 1] - data [0];
			
			return dataRange;
		}
	}
}
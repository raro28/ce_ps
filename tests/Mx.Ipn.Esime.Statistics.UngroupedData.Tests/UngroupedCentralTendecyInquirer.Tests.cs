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
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Mx.Ipn.Esime.Statistics.UngroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;
	
	[TestFixture()]
	public class UngroupedCentralTendecyInquirer_Tests:CentralTendecyInquirerBase_Tests<UngroupedCentralTendecyInquirer,UngroupedHelperMethods>
	{
		protected override double SampleMedian (IList<double> data)
		{
			double result;
			int middleIndex = (data.Count / 2) - 1;
			if ((data.Count % 2) != 0) {
				result = data [middleIndex + 1];
			} else {
				result = (data [middleIndex] + data [middleIndex + 1]) / 2;
			}
			
			return result;
		}
		
		protected override List<double> SampleMode (IEnumerable<double> data)
		{
			var groups = data.GroupBy (item => item);
			var modes = (from _mode in groups
			             where _mode.Count () == groups.Max (grouped => grouped.Count ())
			             select _mode.First ()).ToList ();
			
			return modes;
		}
	}
}
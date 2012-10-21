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
namespace Mx.Ipn.Esime.Statistics.GroupedData.Tests
{
	using System;
	using NUnit.Framework;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.GroupedData;
	using Mx.Ipn.Esime.Statistics.BaseData.Tests;

    [Ignore("appharbor webdeploy")]
	[TestFixture()]
	public class GroupedCentralTendecyInquirer_Tests:CentralTendecyInquirerBase_Tests<GroupedCentralTendecyInquirer,GroupedHelperMethods>
	{
		protected override double SampleMedian (IList<double> data)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMedian
			return -1;
		}

		protected override List<double> SampleMode (IEnumerable<double> data)
		{
			//TODO:GroupedCentralTendecyInquirer_Tests:SampleMode
			return new List<double> ();
		}
	}
}
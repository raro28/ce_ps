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
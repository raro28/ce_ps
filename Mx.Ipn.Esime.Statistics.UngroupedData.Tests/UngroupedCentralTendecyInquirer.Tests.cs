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
		public UngroupedCentralTendecyInquirer_Tests ():base(()=>{return new UngroupedCentralTendecyInquirer (rawData: null);})
		{
		}

		protected override double SampleMedian (IList<double> sortedData)
		{
			double result;
			int middleIndex = (sortedData.Count / 2) - 1;
			if ((sortedData.Count % 2) != 0) {
				result = sortedData [middleIndex + 1];
			} else {
				result = (sortedData [middleIndex] + sortedData [middleIndex + 1]) / 2;
			}
			
			return result;
		}
		
		protected override List<double> SampleMode (IEnumerable<double> sortedData)
		{
			var groups = sortedData.GroupBy (data => data);
			var modes = (from _mode in groups
			             where _mode.Count () == groups.Max (grouped => grouped.Count ())
			             select _mode.First ()).ToList ();
			
			return modes;
		}
	}
}
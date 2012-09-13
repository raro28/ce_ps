namespace Mx.Ipn.Esime.Statistics.Grouped
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class GroupedStatisticsInquirer:StatisticsInquirerBase
	{
		private dynamic DataDistributionFrequencyInquirer {
			get;
			set;
		}

		public GroupedStatisticsInquirer (IList<double> rawData):base(rawData)
		{			
		}
		
		public GroupedStatisticsInquirer (ReadOnlyCollection<double> sortedData):base(sortedData)
		{
		}

		protected override void InitializeInquirers ()
		{
			var dataDistributionFrequencyInquirer = new ExtendedDataDistributionFrequencyInquirer (Data, this);
			DataDistributionFrequencyInquirer = dataDistributionFrequencyInquirer;

			var xilesInquirer = new GroupedXileInquirer (Data, dataDistributionFrequencyInquirer);
			XileInquirer = xilesInquirer;

			var centralTendencyInquirer = new GroupedCentralTendecyInquirer (Data, xilesInquirer);
			CentralTendencyInquirer = centralTendencyInquirer;
			
			RangesInquirer = new GroupedRangesInquirer (Data, xilesInquirer);
			DispersionInquirer = new GroupedDispersionInquirer (Data, CentralTendencyInquirer);
		}

		protected override void InitializeMap ()
		{
			throw new System.NotImplementedException ();
		}
	}
}
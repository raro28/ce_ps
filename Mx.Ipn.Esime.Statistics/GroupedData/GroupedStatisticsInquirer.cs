namespace Mx.Ipn.Esime.Statistics.GroupedData
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
			DataDistributionFrequencyInquirer = new ExtendedDataDistributionFrequencyInquirer (Data, this);
			XileInquirer = new GroupedXileInquirer (Data, this);
			CentralTendencyInquirer = new GroupedCentralTendecyInquirer (Data, this);
			RangesInquirer = new GroupedRangesInquirer (Data, this);
			DispersionInquirer = new GroupedDispersionInquirer (Data, this);
		}

		protected override void FIXME_ExtraMaps ()
		{
			FIXME_TemporalMapper (typeof(IDistributionChartInquirer), DataDistributionFrequencyInquirer);
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class StatisticsInquirer:InquirerBase
	{
		private dynamic CentralTendencyInquirer;
		private dynamic DispersionInquirer;
		private dynamic RangesInquirer;
		private dynamic XileInquirer;

		public StatisticsInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public StatisticsInquirer (ReadOnlyCollection<double> sortedData):base(sortedData)
		{
		}

		protected override void Init ()
		{
			Map = new Dictionary<string, dynamic> ()
			{
				{"GetPercentile",XileInquirer}
			};
			var centralTendencyInquirer = new UCentralTendecyCalculator (Data, this);
			CentralTendencyInquirer = centralTendencyInquirer;
			var xilesInquirer = new UXileCalculator (Data, this);
			XileInquirer = xilesInquirer;

			RangesInquirer = new URangesCalculator (Data, xilesInquirer);
			DispersionInquirer = new UDispersionCalculator (Data, CentralTendencyInquirer);
		}
	}
}


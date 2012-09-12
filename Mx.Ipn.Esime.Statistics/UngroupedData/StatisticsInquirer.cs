namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class StatisticsInquirer:InquirerBase
	{
		private dynamic CentralTendencyInquirer;
		private dynamic DispersionInquirer;
		private dynamic RangesInquirer;
		private dynamic XileInquirer;

		public StatisticsInquirer (ReadOnlyCollection<double> rawData):base(rawData, null)
		{
			Init ();
		}

		void Init ()
		{
			Map = new Dictionary<string, dynamic> ()
			{
				{"GetPercentile",XileInquirer}
			};

			CentralTendencyInquirer = new UCentralTendecyCalculator (Data, this);
			DispersionInquirer = new UDispersionCalculator (Data, this);
			RangesInquirer = new URangesCalculator (Data, this);
			XileInquirer = new UXileCalculator (Data, this);
		}
	}
}


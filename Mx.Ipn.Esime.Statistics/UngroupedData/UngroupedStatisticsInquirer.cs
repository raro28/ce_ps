using Mx.Ipn.Esime.Statistics.Libs;

namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class UngroupedStatisticsInquirer:StatisticsInquirerBase
	{
		public UngroupedStatisticsInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public UngroupedStatisticsInquirer (ReadOnlyCollection<double> sortedData):base(sortedData)
		{
		}

		protected override void InitializeInquirers()
		{
			var centralTendencyInquirer = new UngroupedCentralTendecyInquirer (Data, this);
			CentralTendencyInquirer = centralTendencyInquirer;

			var xilesInquirer = new UngroupedXileInquirer (Data, this);
			XileInquirer = xilesInquirer;
			
			RangesInquirer = new UngroupedRangesInquirer (Data, xilesInquirer);
			DispersionInquirer = new UngroupedDispersionInquirer (Data, CentralTendencyInquirer);
		}

		protected override void InitializeMap ()
		{
			Map = new Dictionary<string, dynamic> ();
			//TODO: fix UngroupedStatisticsInquirer.Init()
			Action<Type,dynamic> action = (type,obj) => {
				foreach (var method in type.GetMethods()) {
					Map.Add (method.Name, obj);
				}
			};
			
			action (typeof(ICentralTendencyInquirer), CentralTendencyInquirer);
			action (typeof(IXileInquirer), XileInquirer);
			action (typeof(IRangesInquirer), RangesInquirer);
			action (typeof(IDispersionInquirer), DispersionInquirer);
		}
	}
}
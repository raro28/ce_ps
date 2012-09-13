namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class StatisticsInquirer:InquirerBase
	{
		private Dictionary<string,dynamic> Map {
			get;
			set;
		}

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

		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			var success = false;
			if (Asked.ContainsKey (binder.Name)) {
				result = Asked [binder.Name];
				success = true;
			} else {
				if (Map.ContainsKey (binder.Name)) {
					var mappedInquirer = Map [binder.Name];
					var name = mappedInquirer.GetType ().Name;
					System.Console.WriteLine (name);
				}
				
				result = null;
			}
			
			return success;
		}

		protected override void Init ()
		{
			Map = new Dictionary<string, dynamic> ()
			{
				{"GetPercentile",XileInquirer}
			};

			var centralTendencyInquirer = new UngroupedCentralTendecyInquirer (Data, this);
			CentralTendencyInquirer = centralTendencyInquirer;
			var xilesInquirer = new UngroupedXileInquirer (Data, this);
			XileInquirer = xilesInquirer;

			RangesInquirer = new UngroupedRangesInquirer (Data, xilesInquirer);
			DispersionInquirer = new UDispersionInquirer (Data, CentralTendencyInquirer);
		}
	}
}


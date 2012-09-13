using Mx.Ipn.Esime.Statistics.Libs;

namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class UngroupedStatisticsInquirer:InquirerBase
	{
		private Dictionary<string,dynamic> Map {
			get;
			set;
		}

		private dynamic CentralTendencyInquirer;
		private dynamic DispersionInquirer;
		private dynamic RangesInquirer;
		private dynamic XileInquirer;

		public UngroupedStatisticsInquirer (IList<double> rawData):base(rawData)
		{			
		}

		public UngroupedStatisticsInquirer (ReadOnlyCollection<double> sortedData):base(sortedData)
		{
		}

		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			var success = false;
			result = null;

			if (Asked.ContainsKey (binder.Name)) {
				result = Asked [binder.Name];
				success = true;
			} else {
				if (Map.ContainsKey (binder.Name)) {
					//TODO: fix UngroupedStatisticsInquirer.TryInvokeMember()
					var mappedInquirer = Map [binder.Name];

					var method = ((Type)mappedInquirer.GetType ()).GetMethod (binder.Name);
					result = method.Invoke (mappedInquirer, args);

					success = true;
				}
			}
			
			return success;
		}

		protected override void Init ()
		{
			var centralTendencyInquirer = new UngroupedCentralTendecyInquirer (Data, this);
			CentralTendencyInquirer = centralTendencyInquirer;
			var xilesInquirer = new UngroupedXileInquirer (Data, this);
			XileInquirer = xilesInquirer;

			RangesInquirer = new UngroupedRangesInquirer (Data, xilesInquirer);
			DispersionInquirer = new UngroupedDispersionInquirer (Data, CentralTendencyInquirer);

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
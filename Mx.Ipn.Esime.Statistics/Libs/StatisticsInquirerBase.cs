namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System;
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public abstract class StatisticsInquirerBase:InquirerBase
	{
		protected Dictionary<string,dynamic> Map {
			get;
			set;
		}
		
		protected dynamic CentralTendencyInquirer {
			get;
			set;
		}

		protected dynamic DispersionInquirer {
			get;
			set;
		}

		protected dynamic RangesInquirer {
			get;
			set;
		}

		protected dynamic XileInquirer {
			get;
			set;
		}
		
		public StatisticsInquirerBase (IList<double> rawData):base(rawData)
		{			
		}
		
		public StatisticsInquirerBase (ReadOnlyCollection<double> sortedData):base(sortedData)
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

		protected abstract void InitializeInquirers ();

		protected abstract void InitializeMap ();

		protected override void Init ()
		{
			InitializeInquirers ();

			InitializeMap ();
		}
	}
}


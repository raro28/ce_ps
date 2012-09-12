namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Dynamic;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public abstract class InquirerBase:DynamicObject,IInquirer
	{
		protected Dictionary<string,dynamic> Map {
			get;
			set;
		}

		public ReadOnlyCollection<double> Data {
			get;
			protected set;
		}

		public Dictionary<string, object> Asked {
			get;
			private set;
		}

		protected dynamic Inquirer {
			get;
			set;
		}

		public InquirerBase (IList<double> rawData, DynamicObject inquirer)
		{
			AssertValidData (rawData);
			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();
			Init (readOnly, inquirer);
		}
		
		public InquirerBase (ReadOnlyCollection<double> sortedData, DynamicObject inquirer)
		{
			AssertValidData (sortedData);
			Init (sortedData, inquirer);
		}

		protected static void AssertValidData (IList<double> data)
		{
			if (data == null) {
				throw new StatisticsException ("Null data set.", new ArgumentNullException ("data"));
			}
			
			if (data.Count == 0) {
				throw new StatisticsException ("Empty data set.");
			}
			
			if (data.Count == 1) {
				throw new StatisticsException ("Insufficient data.");
			}
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
					Console.WriteLine (name);
				}

				result = null;
			}
			
			return success;
		}

		private void Init (ReadOnlyCollection<double> sortedData, DynamicObject inquirer)
		{
			Asked = new Dictionary<string, object> ();
			Data = sortedData;
			if (inquirer != null) {
				Inquirer = inquirer;
				dynamic inq = inquirer;
				Asked = inq.Asked;
			} else {
				Inquirer = this;
			}

			Init ();
		}

		protected virtual void Init ()
		{

		}
	}
}
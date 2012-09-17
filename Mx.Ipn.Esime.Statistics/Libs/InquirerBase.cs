namespace Mx.Ipn.Esime.Statistics.Libs
{
	using System;
	using System.Linq;
	using System.Dynamic;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public abstract class InquirerBase: DynamicObject
	{
		private Dictionary<String, Object> RelatedData {
			get;
			set;
		}

		protected dynamic Inquirer {
			get;
			set;
		}

		public InquirerBase (IList<double> rawData)
		{
			AssertValidData (rawData);
			RelatedData = new Dictionary<string, object> ();

			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();

			RelatedData.Add ("Data", readOnly);

			InitDefaultInquirer ();
		}

		public InquirerBase (InquirerBase inquirer)
		{
			Inquirer = inquirer;
			RelatedData = inquirer.RelatedData;
		}

		protected static void AssertValidData (ICollection<double> data)
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

		public override bool TryGetMember (GetMemberBinder binder, out object result)
		{
			var success = false;
			result = null;
			if (RelatedData.ContainsKey (binder.Name)) {
				result = RelatedData [binder.Name];
				success = true;
			}

			return success;
		}

		public override bool TrySetMember (SetMemberBinder binder, object value)
		{
			if (RelatedData.ContainsKey (binder.Name)) {
				RelatedData [binder.Name] = value;
			} else {
				RelatedData.Add (binder.Name, value);
			}

			return true;
		}

		protected virtual void InitDefaultInquirer ()
		{
			Inquirer = this;
		}
	}
}
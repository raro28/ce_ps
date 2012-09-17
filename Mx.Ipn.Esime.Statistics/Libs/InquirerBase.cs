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
		protected dynamic Inquirer {
			get;
			set;
		}

		protected Dictionary<String, Object> Properties {
			get;
			set;
		}

		public InquirerBase (List<double> rawData)
		{
			AssertValidData (rawData);
			Properties = new Dictionary<string, object> ();

			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();

			Properties.Add ("Data", readOnly);
			Properties.Add ("Answers", new Dictionary<string,dynamic > ());


			Inquirer = this;
		}

		public InquirerBase (InquirerBase inquirer)
		{
			if (inquirer == null)
				throw new StatisticsException ("Null data Inquirer.", new ArgumentNullException ("inquirer"));

			Inquirer = inquirer;

			Properties = Inquirer.Properties;
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

		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			var method = Inquirer.GetType ().GetMethod (binder.Name);
			result = method.Invoke (Inquirer, args);

			return true;
		}

		public override bool TryGetMember (GetMemberBinder binder, out object result)
		{
			var success = false;
			result = null;
			if (Properties.ContainsKey (binder.Name)) {
				result = Properties [binder.Name];
				success = true;
			}

			return success;
		}

		public override bool TrySetMember (SetMemberBinder binder, object value)
		{
			if (Properties.ContainsKey (binder.Name)) {
				Properties [binder.Name] = value;
			} else {
				Properties.Add (binder.Name, value);
			}

			return true;
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class InquirerBase: DynamicObject
	{
		protected dynamic DynamicSelf {
			get {
				return this;
			}
		}

		protected Dictionary<String, dynamic> Properties {
			get;
			set;
		}

		public InquirerBase (IEnumerable<double> rawData)
		{
			AssertValidData (rawData);
			var cache = rawData.ToList ();
			cache.Sort ();
			Properties = new Dictionary<string, dynamic> ()
			{
				{"Inquirers",new List<dynamic>(){this}},
				{"Answers",new Dictionary<string,dynamic > ()},
				{"Data",cache.AsReadOnly ()}
			};

			Properties.Add ("DataPrecision", GetDataPrecision ());
		}

		public InquirerBase (InquirerBase inquirer)
		{
			if (inquirer == null)
				throw new StatisticsException (ExceptionMessages.Null_Data_Inquirer, new ArgumentNullException ("inquirer"));
			Properties = inquirer.Properties;
		}

		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			var success = false;
			result = null;
			foreach (var innerInquirer in Properties["Inquirers"]) {
				var method = innerInquirer.GetType ().GetMethod (binder.Name);
				if (method != null) {
					result = method.Invoke (innerInquirer, args);
					success = true;
					break;
				}
			}

			return success;
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

		protected static void AssertValidData (IEnumerable<double> data)
		{
			if (data == null) {
				throw new StatisticsException (ExceptionMessages.Null_Data_Set, new ArgumentNullException ("data"));
			}
			
			if (data.Count () == 0) {
				throw new StatisticsException (ExceptionMessages.Empty_Data_Set);
			}
			
			if (data.Count () == 1) {
				throw new StatisticsException (ExceptionMessages.Insufficient_Data);
			}
		}

		private int GetDataPrecision ()
		{
			List<double> data = Enumerable.ToList (Properties ["Data"]);
			var decimalLengths = data
				.Distinct ()
				.Where (number => number.ToString ().Contains ('.'))
				.Select (number => number.ToString ().Split ('.') [1].Length)
				.Distinct ()
				.ToList ();

			var maxLenght = decimalLengths.Count () != 0 ? decimalLengths.Max () : 0;

			return maxLenght;
		}
	}
}
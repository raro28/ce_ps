namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

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

		public InquirerBase (List<double> rawData, InquirerBase inquirer=null)
		{
			AssertValidData (rawData);
			Properties = new Dictionary<string, object> ();

			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();

			Inquirer = inquirer ?? this;

			Inquirer.Data = readOnly;
			Inquirer.Answers = new Dictionary<string,dynamic > ();

			Inquirer.DataPresicion = GetDataPresicion ();
		}

		public InquirerBase (InquirerBase inquirer)
		{
			if (inquirer == null)
				throw new StatisticsException (ExceptionMessages.Null_Data_Inquirer, new ArgumentNullException ("inquirer"));

			Inquirer = inquirer;
			Properties = inquirer.Properties;
		}

		protected static void AssertValidData (ICollection<double> data)
		{
			if (data == null) {
				throw new StatisticsException (ExceptionMessages.Null_Data_Set, new ArgumentNullException ("data"));
			}
			
			if (data.Count == 0) {
				throw new StatisticsException (ExceptionMessages.Empty_Data_Set);
			}
			
			if (data.Count == 1) {
				throw new StatisticsException (ExceptionMessages.Insufficient_Data);
			}
		}

		public override bool TryInvokeMember (InvokeMemberBinder binder, object[] args, out object result)
		{
			var success = false;
			result = null;
			var innerInquirer = Inquirer;
			do {
				var method = innerInquirer.GetType ().GetMethod (binder.Name);
				if (method != null) {
					result = method.Invoke (innerInquirer, args);
					success = true;
				}

				innerInquirer = innerInquirer.Inquirer;
			} while(!success&&!innerInquirer.GetType().Equals(GetType()));

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

		private int GetDataPresicion ()
		{
			List<double> data = Enumerable.ToList (Inquirer.Data);

			var decimalLengths = (from item in data.SkipWhile (item => (item + "").LastIndexOf (".") == -1)
				select (item + "").Substring ((item + "").LastIndexOf ('.') + 1).Length).ToList ();

			var maxLenght = decimalLengths.Count () != 0 ? decimalLengths.Max () : 0;

			return maxLenght;
		}
	}
}
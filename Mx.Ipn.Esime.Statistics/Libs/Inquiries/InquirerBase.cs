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
		public ReadOnlyCollection<double> Data {
			get;
			protected set;
		}

		public Dictionary<string, object> Askqued {
			get;
			private set;
		}

		protected dynamic Inquirer {
			get;
			private set;
		}

		public InquirerBase (IList<double> rawData, DynamicObject inquirer)
		{
			AssertValidData (rawData);
			var readOnly = rawData.ToList ().AsReadOnly ();
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

		void Init (ReadOnlyCollection<double> sortedData, DynamicObject inquirer)
		{
			Askqued = new Dictionary<string, object>();
			Data = sortedData;
			Inquirer = inquirer ?? this;
		}
	}
}
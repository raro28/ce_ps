namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Dynamic;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;
	using Mx.Ipn.Esime.Statistics.Libs.Inquiries;

	public abstract class InquirerBase:DynamicObject, IInquirer
	{
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

		public InquirerBase (IList<double> rawData, IInquirer inquirer=null)
		{
			AssertValidData (rawData);
			var cache = rawData.ToList ();
			cache.Sort ();
			var readOnly = cache.AsReadOnly ();
			Init (readOnly, inquirer);
		}
		
		public InquirerBase (ReadOnlyCollection<double> sortedData, IInquirer inquirer=null)
		{
			AssertValidData (sortedData);
			Init (sortedData, inquirer);
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

		private void Init (ReadOnlyCollection<double> sortedData, IInquirer inquirer)
		{
			Asked = new Dictionary<string, object> ();
			Data = sortedData;
			if (inquirer != null) {
				Inquirer = inquirer;
				Asked = inquirer.Asked;
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
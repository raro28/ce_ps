namespace Mx.Ipn.Esime.Statistics.Core.Base
{
    using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class InquirerBase
	{
		public InquirerBase (IEnumerable<double> rawData)
		{
			AssertValidData (rawData);
			var cache = rawData.ToList ();
			cache.Sort ();

			Answers = new Dictionary<string,dynamic > ();
			Data = cache.AsReadOnly ();
			DataPrecision = GetDataPrecision ();
		}

		public Dictionary<string, dynamic> Answers {
			get;
			private set;
		}

		public ReadOnlyCollection<double> Data {
			get;
			private set;
		}

		public int DataPrecision {
			get;
			private set;
		}

		public InquirerBase (InquirerBase inquirer)
		{
			if (inquirer == null)
				throw new StatisticsException (ExceptionMessages.Null_Data_Inquirer, new ArgumentNullException ("inquirer"));
			Answers = inquirer.Answers;
			Data = inquirer.Data;
			DataPrecision = inquirer.DataPrecision;
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
			var decimalLengths = Data
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
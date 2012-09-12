namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Dynamic;
	using System.Collections.Generic;
	using System.Linq;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class URangesCalculator:InquirerBase,IRangesCalculator
	{
		private double? range;
		private double? qRange;
		private double? dRange;
		private double? pRange;

		public URangesCalculator (IList<double> rawData):base(rawData,null)
		{			
		}

		public URangesCalculator (ReadOnlyCollection<double> rawData, DynamicObject inquirer):base(rawData, inquirer)
		{
		}

		public double GetDataRange ()
		{
			if (range == null) {
				range = Data.Max () - Data.Min ();
			}

			return (double)range;
		}

		public double GetInterquartileRange ()
		{
			if (qRange == null) {
				qRange = Inquirer.GetQuartile (3) - Inquirer.GetQuartile (1);
			}
			
			return (double)qRange;
		}

		public double GetInterdecileRange ()
		{
			if (dRange == null) {
				dRange = Inquirer.GetDecile (9) - Inquirer.GetDecile (1);
			}
			
			return (double)dRange;
		}

		public double GetInterpercentileRange ()
		{
			if (pRange == null) {
				pRange = Inquirer.GetPercentile (90) - Inquirer.GetPercentile (10);
			}
			
			return (double)pRange;
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class UngroupedRangesInquirer:InquirerBase,IRangesInquirer
	{
		private double? range;
		private double? qRange;
		private double? dRange;
		private double? pRange;

		public UngroupedRangesInquirer (IList<double> rawData):base(rawData)
		{	
			Inquirer = new UngroupedXileInquirer (rawData);
		}

		public UngroupedRangesInquirer (ReadOnlyCollection<double> sortedData, UngroupedXileInquirer inquirer):base(sortedData, inquirer)
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
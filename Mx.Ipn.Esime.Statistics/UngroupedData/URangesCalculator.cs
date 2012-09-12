namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class URangesCalculator:UBaseCalculator,IRangesCalculator
	{
		private double? range;
		private double? qRange;
		private double? dRange;
		private double? pRange;

		public UXileCalculator XileCalculator {
			get;
			private set;
		}

		public URangesCalculator (List<double> rawData):base(rawData)
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
				qRange = XileCalculator.GetQuartile (3) - XileCalculator.GetQuartile (1);
			}
			
			return (double)qRange;
		}

		public double GetInterdecileRange ()
		{
			if (dRange == null) {
				dRange = XileCalculator.GetDecile (9) - XileCalculator.GetDecile (1);
			}
			
			return (double)dRange;
		}

		public double GetInterpercentileRange ()
		{
			if (pRange == null) {
				pRange = XileCalculator.GetPercentile (90) - XileCalculator.GetPercentile (10);
			}
			
			return (double)pRange;
		}

		protected override void InitCalculator ()
		{
			XileCalculator = new UXileCalculator (Data);
		}
	}
}
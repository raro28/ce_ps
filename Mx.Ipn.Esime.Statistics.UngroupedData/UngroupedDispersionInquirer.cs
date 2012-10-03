namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class UngroupedDispersionInquirer:DispersionInquirerBase
	{
		public UngroupedDispersionInquirer (List<double> rawData):base(rawData)
		{		
			XileInquirer = new UngroupedXileInquirer (this);
		}

		protected override double CalcAbsoluteDeviation (double mean)
		{
			var nAbsDev = 0.0;
			foreach (var item in Data) {
				nAbsDev += Math.Abs (item - mean);
			}

			var mad = nAbsDev / Data.Count;

			return mad;
		}

		protected override double CalcVariance (double mean)
		{
			var nplus1Variance = 0.0;
			foreach (var item in Data) {
				nplus1Variance += Math.Pow ((item - mean), 2);
			}

			var variance = nplus1Variance / (Data.Count - 1);

			return variance;
		}

		protected override double CalcMomentum (int nMomentum, double mean)
		{
			var momentum = 0.0;
			foreach (var item in Data) {
				var meanDiff = (item - mean);
				momentum += Math.Pow (meanDiff, nMomentum);
			}

			momentum /= Data.Count;
			return momentum;
		}

		protected override double CalcDataRange ()
		{
			return Data.Max () - Data.Min ();
		}
	}
}
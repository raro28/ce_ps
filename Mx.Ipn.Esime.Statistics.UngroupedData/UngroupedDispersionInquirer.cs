namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Base;

	public class UngroupedDispersionInquirer:DispersionInquirerBase
	{
		public UngroupedDispersionInquirer (List<double> rawData):base(rawData)
		{		
			var xiles = new UngroupedXileInquirer (this);
			var ranges = new UngroupedRangesInquirer (xiles);
			
			Inquirer = new UngroupedCentralTendecyInquirer (ranges);
		}

		protected override double CalcAbsoluteDeviation ()
		{
			var nAbsDev = 0.0;
			var mean = Inquirer.GetMean ();
			foreach (var item in Inquirer.Data) {
				nAbsDev += Math.Abs (item - mean);
			}

			var mad = nAbsDev / Inquirer.Data.Count;

			return mad;
		}

		protected override double CalcVariance ()
		{
			var nplus1Variance = 0.0;
			var mean = Inquirer.GetMean ();
			foreach (var item in Inquirer.Data) {
				nplus1Variance += Math.Pow ((item - mean), 2);
			}

			var variance = nplus1Variance / (Inquirer.Data.Count - 1);

			return variance;
		}

		protected override double CalcMomentum (int nMomentum)
		{
			var momentum = 0.0;
			var mean = Inquirer.GetMean ();
			foreach (var item in Inquirer.Data) {
				var meanDiff = (item - mean);
				momentum += Math.Pow (meanDiff, nMomentum);
			}
			momentum /= Inquirer.Data.Count;
			return momentum;
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class UngroupedDispersionInquirer:InquirerBase,IDispersionInquirer
	{
		public UngroupedDispersionInquirer (List<double> rawData):base(rawData)
		{		
			var xiles = new UngroupedXileInquirer (this);
			var ranges = new UngroupedRangesInquirer (xiles);
			
			Inquirer = new UngroupedCentralTendecyInquirer (ranges);
		}

		public double GetAbsoluteDeviation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(mad)")) {
				var nAbsDev = 0.0;
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					nAbsDev += Math.Abs (item - mean);
				}

				Inquirer.Answers.Add ("get(mad)", nAbsDev / Inquirer.Data.Count);
			}

			return Inquirer.Answers ["get(mad)"];
		}

		public double GetVariance ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(ssquare)")) {
				var nplus1Variance = 0.0;
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					nplus1Variance += Math.Pow ((item - mean), 2);
				}

				Inquirer.Answers.Add ("get(ssquare)", nplus1Variance / (Inquirer.Data.Count - 1));
			}
			
			return Inquirer.Answers ["get(ssquare)"];
		}

		public double GetStandarDeviation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(s)")) {

				Inquirer.Answers.Add ("get(s)", Math.Sqrt (GetVariance ()));
			}
			
			return Inquirer.Answers ["get(s)"];
		}

		public double GetCoefficientOfVariation ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(cv)")) {
				var strDev = GetStandarDeviation ();
				var mean = Inquirer.GetMean ();

				Inquirer.Answers.Add ("get(cv)", strDev / mean);
			}
			
			return Inquirer.Answers ["get(cv)"];
		}

		public double GetCoefficientOfSymmetry ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(symmetry)")) {
				var m3 = GetMomentum (3);
				var m2 = GetMomentum (2);

				Inquirer.Answers.Add ("get(symmetry)", m3 / Math.Pow (m2, 1.5));
			}

			return Inquirer.Answers ["get(symmetry)"];
		}

		public double GetCoefficientOfKourtosis ()
		{
			if (!Inquirer.Answers.ContainsKey ("get(kourtosis)")) {
				var m4 = GetMomentum (4);
				var m2 = GetMomentum (2);
				
				Inquirer.Answers.Add ("get(kourtosis)", m4 / Math.Pow (m2, 2));
			}
			
			return Inquirer.Answers ["get(kourtosis)"];
		}

		private double GetMomentum (int nMomentum)
		{
			var keyMomentum = String.Format ("get(momentum,{0})", nMomentum);
			if (!Inquirer.Answers.ContainsKey (keyMomentum)) {
				var momentum = 0.0;
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					var meanDiff = (item - mean);
					momentum += Math.Pow (meanDiff, nMomentum);
				}
				
				momentum /= Inquirer.Data.Count;
				Inquirer.Answers.Add (keyMomentum, momentum);
			}

			return Inquirer.Answers [keyMomentum];
		}
	}
}
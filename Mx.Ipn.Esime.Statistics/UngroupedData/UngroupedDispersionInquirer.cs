namespace Mx.Ipn.Esime.Statistics.UngroupedData
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Libs;

	public class UngroupedDispersionInquirer:InquirerBase,IDispersionInquirer
	{
		private double? absoluteDeviation;
		private double? variance;
		private double? standarDeviation;
		private double? coefficientOfVariation;
		private double? coefficientOfSymmetry;
		private double? coefficientOfKourtosis;
		double[] allMomentum;

		public UngroupedDispersionInquirer (IList<double> rawData):base(rawData)
		{		
			Inquirer = new UngroupedCentralTendecyInquirer (rawData);
		}

		public double GetAbsoluteDeviation ()
		{
			if (absoluteDeviation == null) {
				var nAbsDev = 0.0;
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					nAbsDev += Math.Abs (item - mean);
				}

				absoluteDeviation = nAbsDev / Inquirer.Data.Count;
			}

			return (double)absoluteDeviation;
		}

		public double GetVariance ()
		{
			if (variance == null) {
				var nplus1Variance = 0.0;
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					nplus1Variance += Math.Pow ((item - mean), 2);
				}
				
				variance = nplus1Variance / (Inquirer.Data.Count + 1);
			}
			
			return (double)variance;
		}

		public double GetStandarDeviation ()
		{
			if (standarDeviation == null) {

				standarDeviation = Math.Sqrt (GetVariance ());
			}
			
			return (double)standarDeviation;
		}

		public double GetCoefficientOfVariation ()
		{
			if (coefficientOfVariation == null) {
				var strDev = GetStandarDeviation ();
				var mean = Inquirer.GetMean ();

				coefficientOfVariation = strDev / mean;
			}
			
			return (double)coefficientOfVariation;
		}

		public double GetCoefficientOfSymmetry ()
		{
			if (coefficientOfSymmetry == null) {
				var m3 = GetMomentum (3);
				var m2 = GetMomentum (2);
				
				coefficientOfSymmetry = m3 / Math.Pow (m2, 1.5);
			}

			return (double)coefficientOfSymmetry;
		}

		public double GetCoefficientOfKourtosis ()
		{
			if (coefficientOfKourtosis == null) {
				var m4 = GetMomentum (4);
				var m2 = GetMomentum (2);
				
				coefficientOfKourtosis = m4 / Math.Pow (m2, 2);
			}
			
			return (double)coefficientOfKourtosis;
		}

		private double[] GetAllMomemtum ()
		{
			if (allMomentum == null) {
				allMomentum = new double[3];
				var mean = Inquirer.GetMean ();
				foreach (var item in Inquirer.Data) {
					var meanDiff = (item - mean);
					allMomentum [0] += Math.Pow (meanDiff, 2);
					allMomentum [1] += Math.Pow (meanDiff, 3);
					allMomentum [2] += Math.Pow (meanDiff, 4);
				}

				allMomentum [0] /= Inquirer.Data.Count;
				allMomentum [1] /= Inquirer.Data.Count;
				allMomentum [2] /= Inquirer.Data.Count;
			}

			return allMomentum;
		}

		private double GetMomentum (int nMomentum)
		{
			return GetAllMomemtum () [nMomentum - 2];
		}
	}
}
namespace Mx.Ipn.Esime.Statistics.Core.Base
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class DispersionInquirerBase:InquirerBase,IDispersionInquirer
	{
		public DispersionInquirerBase (List<double> rawData):base(rawData)
		{
		}
		
		public DispersionInquirerBase (InquirerBase inquirer):base(inquirer)
		{
		}

		public double GetDataRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.DataRange)) {
				Inquirer.Answers.Add (TaskNames.DataRange, CalcDataRange ());
			}
			
			return Inquirer.Answers [TaskNames.DataRange];
		}
		
		public double GetInterQuartileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.QuartileRange)) {
				Inquirer.Answers.Add (TaskNames.QuartileRange, Inquirer.GetQuartile (3) - Inquirer.GetQuartile (1));
			}
			
			return Inquirer.Answers [TaskNames.QuartileRange];
		}
		
		public double GetInterDecileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.DecileRange)) {
				Inquirer.Answers.Add (TaskNames.DecileRange, Inquirer.GetDecile (9) - Inquirer.GetDecile (1));
			}
			
			return Inquirer.Answers [TaskNames.DecileRange];
		}
		
		public double GetInterPercentileRange ()
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.PercentileRange)) {
				Inquirer.Answers.Add (TaskNames.PercentileRange, Inquirer.GetPercentile (90) - Inquirer.GetPercentile (10));
			}
			
			return Inquirer.Answers [TaskNames.PercentileRange];
		}

		public double GetAbsoluteDeviation (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.AbsoluteDeviation)) {
				Inquirer.Answers.Add (TaskNames.AbsoluteDeviation, CalcAbsoluteDeviation (mean));
			}

			return Inquirer.Answers [TaskNames.AbsoluteDeviation];
		}
		
		public double GetVariance (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.Variance)) {
				Inquirer.Answers.Add (TaskNames.Variance, CalcVariance (mean));
			}

			return Inquirer.Answers [TaskNames.Variance];
		}
		
		public double GetStandarDeviation (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.StandarDeviation)) {
				Inquirer.Answers.Add (TaskNames.StandarDeviation, Math.Sqrt (GetVariance (mean)));
			}

			return Inquirer.Answers [TaskNames.StandarDeviation];
		}

		public double GetCoefficientOfVariation (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.CoefficientOfVariation)) {
				var strDev = GetStandarDeviation (mean);
				var cov = strDev / mean;

				Inquirer.Answers.Add (TaskNames.CoefficientOfVariation, cov);
			}

			return Inquirer.Answers [TaskNames.CoefficientOfVariation];
		}
		
		public double GetCoefficientOfSymmetry (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.CoefficientOfSymmetry)) {
				var m3 = GetMomentum (3, mean);
				var m2 = GetMomentum (2, mean);
				var cos = m3 / Math.Pow (m2, 1.5);

				Inquirer.Answers.Add (TaskNames.CoefficientOfSymmetry, cos);
			}

			return Inquirer.Answers [TaskNames.CoefficientOfSymmetry];
		}
		
		public double GetCoefficientOfKourtosis (double mean)
		{
			if (!Inquirer.Answers.ContainsKey (TaskNames.CoefficientOfKourtosis)) {
				var m4 = GetMomentum (4, mean);
				var m2 = GetMomentum (2, mean);
				var cok = m4 / Math.Pow (m2, 2);

				Inquirer.Answers.Add (TaskNames.CoefficientOfKourtosis, cok);
			}

			return Inquirer.Answers [TaskNames.CoefficientOfKourtosis];
		}
		
		private double GetMomentum (int nMomentum, double mean)
		{
			var keyMomentum = String.Format (TaskNames.MomentumFormat, nMomentum);
			if (!Inquirer.Answers.ContainsKey (keyMomentum)) {
				Inquirer.Answers.Add (keyMomentum, CalcMomentum (nMomentum, mean));
			}
			
			return Inquirer.Answers [keyMomentum];
		}

		protected abstract double CalcAbsoluteDeviation (double mean);

		protected abstract double CalcVariance (double mean);

		protected abstract double CalcMomentum (int nMomentum, double mean);

		protected abstract double CalcDataRange ();
	}
}
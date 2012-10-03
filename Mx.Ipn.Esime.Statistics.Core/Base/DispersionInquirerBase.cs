namespace Mx.Ipn.Esime.Statistics.Core.Base
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class DispersionInquirerBase:InquirerBase,IDispersionInquirer
	{
		protected XileInquirerBase XileInquirer {
			get;
			set;
		}

		public DispersionInquirerBase (List<double> rawData):base(rawData)
		{
		}
		
		public DispersionInquirerBase (XileInquirerBase inquirer):base(inquirer)
		{
		}

		public double GetDataRange ()
		{
			if (!Answers.ContainsKey (TaskNames.DataRange)) {
				Answers.Add (TaskNames.DataRange, CalcDataRange ());
			}
			
			return Answers [TaskNames.DataRange];
		}
		
		public double GetInterQuartileRange ()
		{
			if (!Answers.ContainsKey (TaskNames.QuartileRange)) {
				Answers.Add (TaskNames.QuartileRange, XileInquirer.GetQuartile (3) - XileInquirer.GetQuartile (1));
			}
			
			return Answers [TaskNames.QuartileRange];
		}
		
		public double GetInterDecileRange ()
		{
			if (!Answers.ContainsKey (TaskNames.DecileRange)) {
				Answers.Add (TaskNames.DecileRange, XileInquirer.GetDecile (9) - XileInquirer.GetDecile (1));
			}
			
			return Answers [TaskNames.DecileRange];
		}
		
		public double GetInterPercentileRange ()
		{
			if (!Answers.ContainsKey (TaskNames.PercentileRange)) {
				Answers.Add (TaskNames.PercentileRange, XileInquirer.GetPercentile (90) - XileInquirer.GetPercentile (10));
			}
			
			return Answers [TaskNames.PercentileRange];
		}

		public double GetAbsoluteDeviation (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.AbsoluteDeviation)) {
				Answers.Add (TaskNames.AbsoluteDeviation, CalcAbsoluteDeviation (mean));
			}

			return Answers [TaskNames.AbsoluteDeviation];
		}
		
		public double GetVariance (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.Variance)) {
				Answers.Add (TaskNames.Variance, CalcVariance (mean));
			}

			return Answers [TaskNames.Variance];
		}
		
		public double GetStandarDeviation (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.StandarDeviation)) {
				Answers.Add (TaskNames.StandarDeviation, Math.Sqrt (GetVariance (mean)));
			}

			return Answers [TaskNames.StandarDeviation];
		}

		public double GetCoefficientOfVariation (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfVariation)) {
				var strDev = GetStandarDeviation (mean);
				var cov = strDev / mean;

				Answers.Add (TaskNames.CoefficientOfVariation, cov);
			}

			return Answers [TaskNames.CoefficientOfVariation];
		}
		
		public double GetCoefficientOfSymmetry (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfSymmetry)) {
				var m3 = GetMomentum (3, mean);
				var m2 = GetMomentum (2, mean);
				var cos = m3 / Math.Pow (m2, 1.5);

				Answers.Add (TaskNames.CoefficientOfSymmetry, cos);
			}

			return Answers [TaskNames.CoefficientOfSymmetry];
		}
		
		public double GetCoefficientOfKourtosis (double mean)
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfKourtosis)) {
				var m4 = GetMomentum (4, mean);
				var m2 = GetMomentum (2, mean);
				var cok = m4 / Math.Pow (m2, 2);

				Answers.Add (TaskNames.CoefficientOfKourtosis, cok);
			}

			return Answers [TaskNames.CoefficientOfKourtosis];
		}
		
		private double GetMomentum (int nMomentum, double mean)
		{
			var keyMomentum = String.Format (TaskNames.MomentumFormat, nMomentum);
			if (!Answers.ContainsKey (keyMomentum)) {
				Answers.Add (keyMomentum, CalcMomentum (nMomentum, mean));
			}
			
			return Answers [keyMomentum];
		}

		protected abstract double CalcAbsoluteDeviation (double mean);

		protected abstract double CalcVariance (double mean);

		protected abstract double CalcMomentum (int nMomentum, double mean);

		protected abstract double CalcDataRange ();
	}
}
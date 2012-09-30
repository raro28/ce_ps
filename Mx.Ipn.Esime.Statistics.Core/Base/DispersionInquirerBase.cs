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
			if (!Properties ["Answers"].ContainsKey (TaskNames.DataRange)) {
				Properties ["Answers"].Add (TaskNames.DataRange, CalcDataRange ());
			}
			
			return Properties ["Answers"] [TaskNames.DataRange];
		}
		
		public double GetInterQuartileRange ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.QuartileRange)) {
				Properties ["Answers"].Add (TaskNames.QuartileRange, DynamicSelf.GetQuartile (3) - DynamicSelf.GetQuartile (1));
			}
			
			return Properties ["Answers"] [TaskNames.QuartileRange];
		}
		
		public double GetInterDecileRange ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.DecileRange)) {
				Properties ["Answers"].Add (TaskNames.DecileRange, DynamicSelf.GetDecile (9) - DynamicSelf.GetDecile (1));
			}
			
			return Properties ["Answers"] [TaskNames.DecileRange];
		}
		
		public double GetInterPercentileRange ()
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.PercentileRange)) {
				Properties ["Answers"].Add (TaskNames.PercentileRange, DynamicSelf.GetPercentile (90) - DynamicSelf.GetPercentile (10));
			}
			
			return Properties ["Answers"] [TaskNames.PercentileRange];
		}

		public double GetAbsoluteDeviation (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.AbsoluteDeviation)) {
				Properties ["Answers"].Add (TaskNames.AbsoluteDeviation, CalcAbsoluteDeviation (mean));
			}

			return Properties ["Answers"] [TaskNames.AbsoluteDeviation];
		}
		
		public double GetVariance (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.Variance)) {
				Properties ["Answers"].Add (TaskNames.Variance, CalcVariance (mean));
			}

			return Properties ["Answers"] [TaskNames.Variance];
		}
		
		public double GetStandarDeviation (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.StandarDeviation)) {
				Properties ["Answers"].Add (TaskNames.StandarDeviation, Math.Sqrt (GetVariance (mean)));
			}

			return Properties ["Answers"] [TaskNames.StandarDeviation];
		}

		public double GetCoefficientOfVariation (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.CoefficientOfVariation)) {
				var strDev = GetStandarDeviation (mean);
				var cov = strDev / mean;

				Properties ["Answers"].Add (TaskNames.CoefficientOfVariation, cov);
			}

			return Properties ["Answers"] [TaskNames.CoefficientOfVariation];
		}
		
		public double GetCoefficientOfSymmetry (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.CoefficientOfSymmetry)) {
				var m3 = GetMomentum (3, mean);
				var m2 = GetMomentum (2, mean);
				var cos = m3 / Math.Pow (m2, 1.5);

				Properties ["Answers"].Add (TaskNames.CoefficientOfSymmetry, cos);
			}

			return Properties ["Answers"] [TaskNames.CoefficientOfSymmetry];
		}
		
		public double GetCoefficientOfKourtosis (double mean)
		{
			if (!Properties ["Answers"].ContainsKey (TaskNames.CoefficientOfKourtosis)) {
				var m4 = GetMomentum (4, mean);
				var m2 = GetMomentum (2, mean);
				var cok = m4 / Math.Pow (m2, 2);

				Properties ["Answers"].Add (TaskNames.CoefficientOfKourtosis, cok);
			}

			return Properties ["Answers"] [TaskNames.CoefficientOfKourtosis];
		}
		
		private double GetMomentum (int nMomentum, double mean)
		{
			var keyMomentum = String.Format (TaskNames.MomentumFormat, nMomentum);
			if (!Properties ["Answers"].ContainsKey (keyMomentum)) {
				Properties ["Answers"].Add (keyMomentum, CalcMomentum (nMomentum, mean));
			}
			
			return Properties ["Answers"] [keyMomentum];
		}

		protected abstract double CalcAbsoluteDeviation (double mean);

		protected abstract double CalcVariance (double mean);

		protected abstract double CalcMomentum (int nMomentum, double mean);

		protected abstract double CalcDataRange ();
	}
}
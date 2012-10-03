namespace Mx.Ipn.Esime.Statistics.Core.Base
{
	using System;
	using System.Collections.Generic;
	using Mx.Ipn.Esime.Statistics.Core.Resources;

	public abstract class DispersionInquirerBase:InquirerBase,IDispersionInquirer
	{
		public DispersionInquirerBase (IEnumerable<double> rawData):base(rawData)
		{
		}

		protected XileInquirerBase XileInquirer {
			get;
			set;
		}

		protected CentralTendecyInquirerBase CentralTendecyInquirer {
			get;
			set;
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

		public double GetAbsoluteDeviation ()
		{
			if (!Answers.ContainsKey (TaskNames.AbsoluteDeviation)) {
				Answers.Add (TaskNames.AbsoluteDeviation, CalcAbsoluteDeviation ());
			}

			return Answers [TaskNames.AbsoluteDeviation];
		}
		
		public double GetVariance ()
		{
			if (!Answers.ContainsKey (TaskNames.Variance)) {
				Answers.Add (TaskNames.Variance, CalcVariance ());
			}

			return Answers [TaskNames.Variance];
		}
		
		public double GetStandarDeviation ()
		{
			if (!Answers.ContainsKey (TaskNames.StandarDeviation)) {
				Answers.Add (TaskNames.StandarDeviation, Math.Sqrt (GetVariance ()));
			}

			return Answers [TaskNames.StandarDeviation];
		}

		public double GetCoefficientOfVariation ()
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfVariation)) {
				var strDev = GetStandarDeviation ();
				var cov = strDev / CentralTendecyInquirer.GetMean ();

				Answers.Add (TaskNames.CoefficientOfVariation, cov);
			}

			return Answers [TaskNames.CoefficientOfVariation];
		}
		
		public double GetCoefficientOfSymmetry ()
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfSymmetry)) {
				var m3 = GetMomentum (3);
				var m2 = GetMomentum (2);
				var cos = m3 / Math.Pow (m2, 1.5);

				Answers.Add (TaskNames.CoefficientOfSymmetry, cos);
			}

			return Answers [TaskNames.CoefficientOfSymmetry];
		}
		
		public double GetCoefficientOfKourtosis ()
		{
			if (!Answers.ContainsKey (TaskNames.CoefficientOfKourtosis)) {
				var m4 = GetMomentum (4);
				var m2 = GetMomentum (2);
				var cok = m4 / Math.Pow (m2, 2);

				Answers.Add (TaskNames.CoefficientOfKourtosis, cok);
			}

			return Answers [TaskNames.CoefficientOfKourtosis];
		}
		
		private double GetMomentum (int nMomentum)
		{
			var keyMomentum = String.Format (TaskNames.MomentumFormat, nMomentum);
			if (!Answers.ContainsKey (keyMomentum)) {
				Answers.Add (keyMomentum, CalcMomentum (nMomentum));
			}
			
			return Answers [keyMomentum];
		}

		protected abstract double CalcAbsoluteDeviation ();

		protected abstract double CalcVariance ();

		protected abstract double CalcMomentum (int nMomentum);

		protected abstract double CalcDataRange ();
	}
}